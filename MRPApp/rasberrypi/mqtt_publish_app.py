# 라이브러리 추가
import time
import datetime as dt
from typing import OrderedDict
import RPi.GPIO as GPIO
import paho.mqtt.client as mqtt
import json

s2 = 23     # Raspberry Pi Pin 23
s3 = 24     # Raspberry Pi Pin 24
out = 22    # sensing Pin 22 # 25핀 문제생김 -> 22번
NUM_CYCLES = 10

dev_id = 'MACHINE01'
broker_address = '192.168.0.5'  # 내 컴퓨터 IP
publish_topic = 'factory1/machine1/data/'

def send_data(param, red, green, blue):
    message = ''
    if param == 'GREEN':
        message = 'OK'
    elif param == 'RED':
        message = 'FAIL'
    elif param == 'CONN':
        message = 'CONNECTED'
    else:
        message = 'ERR'

    currtime = dt.datetime.now().strftime('%Y-%m-%d %H:%M:%S.%f')
    # json data generate
    raw_data = OrderedDict()
    raw_data['DEV_ID'] = dev_id
    raw_data['PRC_TIME'] = currtime
    raw_data['PRC_MSG'] = message

    raw_data['PARAM'] = param
    raw_data['RED'] = red
    raw_data['GREEN'] = green
    raw_data['BLUE'] = blue

    pub_data = json.dumps(raw_data, ensure_ascii=False, indent='\t')
    print(pub_data)
    # mqtt_publish
    client2.publish(publish_topic, pub_data)

def read_value(a2, a3):
    GPIO.output(s2, a2)
    GPIO.output(s3, a3)
    
    #센서 조정시간 설정
    time.sleep(0.3)
    start = time.time()
    for impulse_count in range(NUM_CYCLES):
        GPIO.wait_for_edge(out, GPIO.FALLING)   
    end = (time.time() - start)
    return NUM_CYCLES / end

def setup():
    ## GPIO Setting
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(s2, GPIO.OUT)
    GPIO.setup(s3, GPIO.OUT)
    GPIO.setup(out, GPIO.IN, pull_up_down = GPIO.PUD_UP) # 센서결과 받기

def loop():
    ## 반복하면서 일처리
    result = ''

    while True:
        red = read_value(GPIO.LOW, GPIO.LOW)    # s2 low, s3 low
        time.sleep(0.1) # 0.1초 딜레이
        green = read_value(GPIO.HIGH, GPIO.HIGH)    # s2 high, s3 high
        time.sleep(0.1)
        blue = read_value(GPIO.LOW, GPIO.HIGH)

        print('red = {0}, green = {1}, blue = {2}'.format(red,green,blue))

        if(red<50) : continue
        # if(red>2000 or green>2000 or blue>2000): continue

        if (red > green) and (red > blue):
            result = 'RED'
            send_data(result, red, green, blue)
        elif (green > red) and (green > blue):
            result = 'GREEN'
            send_data(result, red, green, blue)
        else:
            result = 'ERR'
            send_data(result, red, green, blue)

        # send_data(result)
        time.sleep(1)

# MQTT 초기화
client2 = mqtt.Client(dev_id)
client2.connect(broker_address)
print('MQTT client connected')

if __name__ == '__main__': ## 메인함수
    setup()
    send_data('CONN', None, None, None)   # MQTT 접속 성공 메시지 전달

    try:
        loop()
    except KeyboardInterrupt:
        GPIO.cleanup()
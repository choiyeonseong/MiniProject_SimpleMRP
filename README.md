# MiniProject_SimpleMRP
SmartFactory 공정관리 with RaspberryPi

## 공정 과정 
</br>
<img src="readme_img/sensor.jpg" width="450"></img>

- color sensor을 이용해서 색깔별로 분류
- red : FAIL
- green : OK
- blud : ERR
- MQTT Publish
- json형식으로 센싱 결과 전송

[sensor](MRPApp/rasberrypi/mqtt_publish_app.py)

## Device Subscriber
</br>
<img src="readme_img/mqttsubscribe.png" width="450"></img>

- json형식으로 전송된 메시지를 수신
- connection string, client id, topic을 설정하여 MQTT 연결
- 받아온 값을 데이터베이스에 저장
- 연결, 연결 해제 기능 버튼

[Device Subscriber](MRPApp/DeviceSubApp)

## 공정 모니터링 
1. 공정 계획 
</br>
<img src="readme_img/schedules.png" width="450"></img>

- 공정일자에 따른 공장, 로드타임, 공정시간, 공정설비, 계획수량을 입력, 수정하고 DB에 저장

[공정계획](MRPApp/MRPApp/View/Schedule/)
 
2. 공정 모니터링 
</br>
<img src="readme_img/beforemonitoring.png" width="450"></img><br/>
<img src="readme_img/aftersensing.png" width="450"></img>

- 해당 공정일자의 정보를 받아와서 화면에 표시
- 센싱결과를 읽어와서 공정 결과를 표시하고 데이터를 DB에 저장
- 애니메이션을 이용해 공정과정을 보여주고 실제 공정 결과에 따라 색상이 변경

[공정모니터링](MRPApp/MRPApp/View/Process/)

3. 리포트 
</br>
<img src="readme_img/reportview.png" width="450"></img>

- 공정일 기준으로 계획수량, 성공수량, 실패수량을 그래프로 표현

[리포트](MRPApp/MRPApp/View/Report/)

4. 설정 
</br>
<img src="readme_img/settingview.png" width="450"></img>

- 설비, 공장에 대한 정보를 코드, 코드명, 코드설명으로 입력, 수정하고 DB에 저장

[설정](MRPApp/MRPApp/View/Setting/)

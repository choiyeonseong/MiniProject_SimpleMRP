# MiniProject_SimpleMRP
SmartFactory 공정관리 with RaspberryPi
- [Device Subscriber](#device-subscriber)
- [공정 과정](#공정-과정)
- [공정 모니터링](#공정-모니터링)
- [EntityFramework](#entityframework)
- [Commons.cs](#commonscs)
------

## [Device Subscriber](MRPApp/DeviceSubApp)

<img src="readme_img/mqttsubscribe.png" height="80%" width="80%"></img>

- json형식의 메시지를 수신
- connection string, client id, topic을 설정하여 MQTT 연결
    ```C#
    /* MQTT Subscribe */
    
    client = new MqttClient(brokerAddress);
        client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
    
    client.Connect(client_id);   // MQTT연결
    
    client.Subscribe(new string[] { /* Topic */ },
                new byte[] { /* received_message */ });  // QoS 0
                
    client.Disconnect();    // MQTT 연결 해제
    ```
- 연결, 연결 해제 기능 버튼

## [공정 과정](MRPApp/rasberrypi/mqtt_publish_app.py)

<img src="readme_img/sensor.jpg" height="80%" width="80%"></img>

- color sensor을 이용해서 색깔별로 분류
- red : FAIL
- green : OK
- blud : ERR
- MQTT Publish
   ```Python
   ## MQTT Publish 과정
   
   client = mqtt.Client(client_id)   # 새로운 클라이언트 생성
   client.connect(broker_address) # 브로커에 접속
   
   client.publish(publish_topic, message)  # 토픽으로 메시지 발행
   ```
- json형식으로 센싱 결과 전송

## 공정 모니터링 

1. [공정 계획](MRPApp/MRPApp/View/Schedule/)

    <img src="readme_img/schedules.png" height="80%" width="80%"></img>

    - 공정일자에 따른 공장, 로드타임, 공정시간, 공정설비, 계획수량을 입력, 수정하고 DB에 저장

2. [공정 모니터링](MRPApp/MRPApp/View/Process/)

    <img src="readme_img/beforemonitoring.png" height="80%" width="80%"></img><br/>
    <img src="readme_img/aftersensing.png" height="80%" width="80%"></img>

    - 공정계획에서 오늘의 생산 계획 일정 불러옴
    - MQTT Subscription 연결해서 센싱결과를 읽어옴 
    - 애니메이션을 이용해 공정과정을 보여주고 실제 로드타임 동안 실행
        ```C#
        /* 애니메이션 동작 */
        
        // 제품 애니메이션 속성
        DoubleAnimation ma = new DoubleAnimation();

        ma.From = 138;  // 시작 위치
        ma.To = 525;    // 옮겨지는 x값의 최대값
        ma.Duration = TimeSpan.FromSeconds(currSchedules.SchLoadTime);  // 일정 계획 로드타임
        //ma.AccelerationRatio = 0.5;   // 가속
        //ma.AutoReverse = true;        // 왕복 운동

        Product.BeginAnimation(Canvas.LeftProperty, ma);    // 애니메이션 속성 설정
        ```
    - 공정 결과에 따라 물체의 색상이 변경(Red/Green)
    - 공정 결과를 표시하고 데이터를 DB에 저장

3. [리포트](MRPApp/MRPApp/View/Report/)

    <img src="readme_img/reportview.png" height="80%" width="80%"></img>

    - 선택한 공정일의 계획수량, 성공수량, 실패수량을 그래프로 표현
        ```C#
        /* 차트 표시 */
        
        // 데이터를 배열에 가져옴
        int[] schAmounts = list.Select(a => (int)a.SchAmount).ToArray();    
        
        // 차트 속성 설정
        var series1 = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "계획수량",
                Fill = new SolidColorBrush(Colors.Green),
                Values = new LiveCharts.ChartValues<int>(schAmounts)
            };
            
        // 차트 할당
        ChtReport.Series.Add(series1);  
        
        // x축 좌표값을 날짜(PrcDate)로 표시
        ChtReport.AxisX.First().Labels = list.Select(a => a.PrcDate.ToString("yyyy-MM-dd")).ToList();
        ```
    - 날짜 검색 기능(기간으로도 설정 가능)

4. [설정](MRPApp/MRPApp/View/Setting/)

    <img src="readme_img/settingview.png" height="80%" width="80%"></img>

    - 설비, 공장에 대한 정보를 코드, 코드명, 코드설명으로 입력, 수정하고 DB에 저장

------

* <h3>EntityFramework</h3>

```C#
// Set,Get 함수는 Logic/DataAccess.cs 에 생성 

/* SELECT example */
var plantCodes = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("PC01")).ToList();

/* UPDATE example */
var result = Logic.DataAccess.SetSchedules(item);

/* INSERT example */
var item = new Model.Schedules();   // CREATE
// Fill
var result = Logic.DataAccess.SetSchedules(item);   // INSERT
```

[참고자료](http://www.csharpstudy.com/web/article/8-Entity-Framework)

* <h3>Commons.cs</h3>

```C#
/* MetroWindow에서 비동기 알림 메시지 표시 */

public static async Task<MessageDialogResult> ShowMessageAsync(
    string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
{
   return await ((MetroWindow)Application.Current.MainWindow)
        .ShowMessageAsync(title, message, style, null);
}
```

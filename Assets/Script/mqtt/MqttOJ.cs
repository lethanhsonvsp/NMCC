using MQTTnet;
using MQTTnet.Protocol;
using UnityEngine;

public class MqttOJ : MonoBehaviour
{
    public MqttClientController mqttClientController;
    public string topic = "unity/position";

    void Update()
    {
        if (mqttClientController != null && mqttClientController.IsConnected)
        {
            Vector3 position = transform.position;
            string message = JsonUtility.ToJson(position);

            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce)
                .Build();

            mqttClientController.mqttClient.PublishAsync(mqttMessage);
        }
        else
        {
            //Debug.LogWarning("MQTT client is not connected. Skipping publish.");
        }
    }
}

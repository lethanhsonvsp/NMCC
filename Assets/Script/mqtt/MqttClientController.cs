using UnityEngine;
using MQTTnet;
using MQTTnet.Client;
using System.Threading.Tasks;
using System.Text;

public class MqttClientController : MonoBehaviour
{
    public string mqttBrokerAddress = "broker.hivemq.com";
    public int mqttPort = 1883;
    public IMqttClient mqttClient { get; private set; }
    public bool IsConnected => mqttClient?.IsConnected ?? false;

    async void Start()
    {
        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithClientId("UnityClient")
            .WithTcpServer(mqttBrokerAddress, mqttPort)
            .WithCleanSession()
            .Build();

        mqttClient.ConnectedAsync += async e =>
        {
            Debug.Log("Connected to MQTT Broker.");
            await Task.CompletedTask;
        };

        mqttClient.DisconnectedAsync += async e =>
        {
            Debug.Log("Disconnected from MQTT Broker.");
            await Task.CompletedTask;
        };

        try
        {
            await mqttClient.ConnectAsync(options);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"MQTT connection failed: {ex.Message}");
        }
    }
}

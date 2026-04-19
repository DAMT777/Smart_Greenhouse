#include <Arduino.h>

int sensorPin = A0;
int bombaPin = 7;

void setup() {
    pinMode(bombaPin, OUTPUT);
    digitalWrite(bombaPin, LOW);
    Serial.begin(9600);
}

void loop() {
    if (Serial.available() > 0) {
        String comando = Serial.readStringUntil('\n');
        comando.trim();

        if (comando == "READ_HUMEDAD") {
            int rawValue = analogRead(sensorPin);
            float humedad = map(rawValue, 0, 1023, 0, 100);
            Serial.println(humedad);
        }
        else if (comando == "BOMBA_ON") {
            digitalWrite(bombaPin, HIGH);
            Serial.println("OK_BOMBA_ON");
        }
        else if (comando == "BOMBA_OFF") {
            digitalWrite(bombaPin, LOW);
            Serial.println("OK_BOMBA_OFF");
        }
        else {
            Serial.println("ERROR_COMANDO_DESCONOCIDO");
        }
    }
}

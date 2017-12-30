

void dht_setup() {

  dht.begin();
}

float read_temp() {


  float h = dht.readHumidity();

  float t = dht.readTemperature();

  if (isnan(h) || isnan(t)) {
    Serial.println("Failed to read from DHT sensor!");
    return;
  }



  return t;
}

float read_humm(){
  float h=dht.readHumidity();
  return h;
}



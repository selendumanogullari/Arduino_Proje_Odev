float humm_gnd(){
  float humm_g=analogRead(9);
  float humm_percent=(100-(humm_g/1024)*100)*10;
  return humm_percent;
}


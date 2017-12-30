#include <LiquidCrystal.h>
LiquidCrystal lcd(8,9,4,5,6,7);
int role_pin=52;
float sicaklik;
float nem_hava;
float nem_toprak;
String msg_temp;
bool r1=true;
int up_time=0;
#include "DHT.h"

#define DHTPIN 22     // what digital pin we're connected to

#define DHTTYPE DHT11   // DHT 11

DHT dht(DHTPIN, DHTTYPE);

void setup(){
  lcd_setup();
  role_setup(role_pin);
  Serial.begin(9600);
  dht_setup();
  
  
}

void loop(){
  sicaklik=read_temp();
  nem_hava=read_humm();
  nem_toprak=humm_gnd();
  lcd_write(sicaklik,nem_hava,nem_toprak);
  delay(500);
  read_temp();
  //Pin donanım gereği low durumunda röleyi açar
  if(sicaklik>5 && nem_toprak<40) r1=false;  else r1=true;
  String msg=Serial.readString();
  lcd_write_msg(msg);
  role_set(r1,role_pin);

  Serial.print(sicaklik,0);
  Serial.print(" ");
  Serial.print(nem_hava,0);
  Serial.print(" ");
  Serial.print(nem_toprak,0);
  Serial.print(" ");
  if(r1=false) { int i=1;
   Serial.print(i); }
  else if(r1=true) {int i=0;
   Serial.print(i);}
  Serial.print(" ");
  up_time++;
  Serial.println(up_time);
  delay(800);
  
}


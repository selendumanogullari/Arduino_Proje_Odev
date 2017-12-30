void role_setup(int rn1){
  pinMode(rn1,OUTPUT);
}
void role_set(bool r1,int rn1){
  if(r1==false)
    digitalWrite(rn1,HIGH);
  else if(r1==true)
    digitalWrite(rn1,LOW);
}

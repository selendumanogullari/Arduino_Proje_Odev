void lcd_setup() {
  lcd.begin(16, 2); // LCD ekran arayüzü başlatır ve ekran boyutları (genişlik ve yükseklik) belirtir
  lcd.print("selenyum");   // Ekrana Yazı Yazdırılır.
  lcd.setCursor(0, 1);    // ekranın alt satırına yazı yazdırmak için cursor konumlandırılır.
  lcd.clear(); // ekran tamamen silinir.
}
void lcd_write(int temp, int nem,int toprak_nem) {
  lcd.setCursor(0,0);
  lcd.print("               ");
  lcd.setCursor(0, 0);
  lcd.print(temp);
  lcd.setCursor(2,0);
  lcd.print("C");
  lcd.setCursor(4, 0);
  lcd.print("%");
  lcd.setCursor(5,0);
  lcd.print(nem);
  lcd.setCursor(8,0);
  lcd.print("%");
  lcd.setCursor(9,0);
  lcd.print(toprak_nem);
  
}
void lcd_write_msg(String msg) {
  if(!msg.equals(msg_temp)){
  lcd.clear();
  lcd.setCursor(0, 1);
  lcd.print(msg);
  }
  else
    return 0;
    
}

# Audiometer

Projekt ten pozwala na przeprowadzenie podstawowego badania słuchu. Niezbędny do odpalenia tego programu jest układ z Arduino, które 
jest podpięte do portu USB. Układ ten składać się może z płytki stykowej, przycisku oraz Arduino. Arduino musi być skonfigurowane w taki 
sposób, aby co 100ms przesyłać dane do komputera. Jeśli Arduino jest poprawnie skonfigurowane można ropocząć badanie. Polega ono 
na odtwarzaniu dźwięków osobno dla każdego o częstotliwościach od 250 Hz do 4 kHz. W trakcie badania natężenie dźwięku wzrasta. 
Zadaniem osoby badanej jest wciśnięcie przycisku w momencie usłyszenia dźwięku. Po przeprowadzeniu badania generowany jest
audiogram ukazujący ubytki słuchu osoby badanej. 

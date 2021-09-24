# ransomware-jester 

es un nuevo tipo de ransomware  


¿Cómo funciona?
Jester funciona como una cadena de ejecutables , que cuando se ejecuta realizará los siguientes pasos:

jester pasará por el sistema de archivos en busca de archivos personales con ciertas extensiones (es decir, jpeg, png, docx, txt, xls, etc.) (esto se puede cambiar en program.cs).
Los archivos que coincidan con la lista de extensiones se cifrarán utilizando AES256 con la contraseña "hunter" (esto se puede cambiar en program.cs).
Eliminará todas las instantáneas del sistema afectado, si jester se ha ejecutado con derechos administrativos.
Se realiza una edición en el registro, indicándole al sistema que ejecute jester.exe nuevamente al inicio. (Esto no funciona cuando se ejecuta a través de cobalt-strikes execute-assembly si el sistema no logra iniciarlo no hay problema jester cuenta con un tercer archivo capaz de hacer que inicie jester automáticamente 

Se muestra una "interfaz de usuario emergente de rescate" que informa al usuario cuántos archivos se han cifrado. La ventana emergente también contiene un campo de contraseña, que permite descifrar los archivos.
 ![image](https://user-images.githubusercontent.com/91295669/134601820-bef2cb24-8bf7-485a-8efd-a87e8825d6f9.png)

Ransomware sin extensión
Muchos programas de ransomware cifrarán archivos y cambiarán la extensión del archivo original a algo como. Krab, .ppam, .trumphead. etc. La razón por la que la mayoría de los programas de ransomware cambian las extensiones de archivo es para saber qué archivos descifrar (si es necesario). Jester se diferencia de la mayoría de los ransomware al mantener la extensión del archivo original. Por lo tanto, los archivos tendrán el mismo aspecto, sin embargo, ninguno de ellos funcionará como se esperaba.
Cuando el tiempo se halla acabado jester borrara todos los archivos cifrados en el sistema 

Ransomware auto descargador 
JESTER cuenta con un archivo que auto descarga todas las partes que necesita jester para ser totalmente invulnerable a cualquier antivirus conocido 
Viene con un archivo auto descargador
![image](https://user-images.githubusercontent.com/91295669/134601890-37372bf3-5071-4553-965a-deb1cad3ceea.png)
jester iniciara  despues de que se hallan descargado los archivos el auto descargador pesa alrededor de 12 KB 
 
Jester cuenta con auto kill de procesos cuando alguien intente abrir cmd se cerrará automáticamente 
Aparte de que no se podrá usar el administrador de tarea para cerrar jester
![image](https://user-images.githubusercontent.com/91295669/134602445-8e069a7f-0372-4642-a91a-8cd1642e4355.png)

mi nombre es hunter dexter  tengo  un grupo de seguridad informatica que esperaras para unirte
https://chat.whatsapp.com/FduNfEHQ0U3Kc3aME43wY3

 
Jester cuenta con 3 archivos el descargador auto inicio y el ransomware jester


DESCARGO DE RESPONSABILIDAD


Esto es como el ransomware Jester, en la película Mr. Robot (codificada por hunter dexter), tenga cuidado este es un ransomware real. No utilice este malware en su computadora personal. Este proyecto es solo está basado en la serie de MR. robot 



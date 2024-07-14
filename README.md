# BestSecondPrice

## Überblick
BestSecondPrice ist eine Software, die prüft, ob Bücher als Mängelexemplare auf den Webseiten Arvelle und Cheebo im Angebot sind. Das Programm durchsucht die angegebenen Webseiten nach den gewünschten Büchern und informiert den Benutzer, wenn diese im Angebot sind.

## Voraussetzungen

- .NET Framework
- Internetverbindung
- Eine Textdatei namens `Suchbegriffe.txt` im selben Verzeichnis wie die ausführbare Datei (`.exe`). Diese Datei sollte die Suchbegriffe enthalten, wobei jeder Begriff in einer neuen Zeile steht.

## Installation und Ausführung

1. **Erstellung der Suchbegriffe-Datei**
   - Erstelle eine Textdatei namens `Suchbegriffe.txt` im Verzeichnis der ausführbaren Datei.
   - Trage die zu suchenden Buchtitel in die Datei ein, wobei jeder Titel in einer neuen Zeile steht.

2. **Starten des Programms**
   - Die Software muss mit fünf Kommandozeilenparametern gestartet werden:
     1. **Sender E-Mail**: Die E-Mail-Adresse, von der die Benachrichtigung gesendet wird.
     2. **Passwort**: Das Passwort der Sender-E-Mail.
     3. **SMTP-Server**: Der SMTP-Server der Sender-E-Mail.
     4. **Port**: Der Port für den SMTP-Server.
     5. **Target E-Mail**: Die Ziel-E-Mail-Adresse, an die die Benachrichtigungen gesendet werden.

   - Beispiel für den Aufruf:
     ```sh
     BestSecondPrice.exe "deine.email@example.com" "deinPasswort" "smtp.example.com" "587" "ziel.email@example.com"
     ```

3. **Ausgabe und Benachrichtigung**
   - Wenn ein Buch im Angebot ist, wird dies in der Kommandozeile (CMD) ausgegeben.
   - Zusätzlich wird eine E-Mail an die Ziel-E-Mail-Adresse gesendet, die alle Bücher auflistet, die im Angebot sind.

## Empfehlungen

- **Automatisierung**: Es wird empfohlen, die Software auf einem Server auszuführen oder sie in der Aufgabenplanung zu hinterlegen, um regelmäßige Überprüfungen durchzuführen.

## Beispiel-Suchbegriffe.txt

```txt
Harry Potter und der Stein der Weisen
Der Herr der Ringe
Die Tribute von Panem

# Music.Lyrics.Word.Counter

This will return some stats of the music lyrics of artists that are specified. It uses the musicbrainz api to get all the song titles of an artist and the lyrics.ovh for the music lyrics.

## Dependencies
* .NET 5 SDK

## Run Instructions using CLI
1. Open a command prompt or terminal
2. Navigate to */Lyrics.Counter
3. Enter this command `dotnet run`
4. Open a browser
5. Enter this URL `http://localhost:5000/`

## Run Instructions using Visual Studio
1. Open Visual Studio
2. Open the app solution
3. Ensure you have IIS Express selected
4. Press the play button

## Potential Improvements
* Use fluent validation
* Maybe use caching to improve the lyrics api performance
* Could a cancel button that will cancel the search for finding the song lyrics. This will mean the lyrics average will less accurate.
* Find a way to unit test the logic in the razor pages. Maybe by extracting the logic to another helper class.
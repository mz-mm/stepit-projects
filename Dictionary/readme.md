## Project Task

Create a Dictionaries application.
The main task of the project: to store dictionaries in different languages and allow users
help you find the translation of the desired word or phrase.
The application interface should provide the following capabilities:
- Create a dictionary. When creating, you need to specify the type of dictionary.
For example, English-Russian or Russian-English.
- Add a word and its translation to an existing dictionary. Since the word has a
There may be several translations, it is necessary to support the ability to create
several translation options.
- Replace a word or its translation in the dictionary.
- Delete a word or translation. If a word is deleted, all its translations are deleted
with him. You cannot delete a translation of a word if it is the last version of the translation.
water.
- Look for the translation of a word.
- Dictionaries should be stored in files.
- The word and its translation options can be exported to a separate result file
tata.
- When starting the program, it is necessary to show the menu for working with the program.
  If selecting a menu item opens a submenu, then it must include
  the third option is to return to the previous menu.


### Project Notes
- The application's user interface (UI) is functional but may not provide the best user experience since the use of a frontend framework was not within the project's scope.
- The implementation of the database for storing dictionaries is functional, but it may not be optimized for production use. Working with a more robust database system like PostgreSQL was not considered within the project's scope.
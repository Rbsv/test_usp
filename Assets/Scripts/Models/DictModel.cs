using System.Collections.Generic;

[System.Serializable]
public class DictModel
{
    public string word;
    public List<Phonetic> phonetics;
    public List<Meaning> meanings;
    public License license; //useless
    public List<string> sourceUrls;//useless
}

[System.Serializable]
public class Phonetic
{
    public string text;
    public string audio;
}

[System.Serializable]
public class Meaning
{
    public string partOfSpeech;
    public List<DefinitionData> definitions;
    public List<string> synonyms;
    public List<string> antonyms;
}

[System.Serializable]
public class DefinitionData
{
    public string definition;
    public string example; 
    public List<string> synonyms;
    public List<string> antonyms;
}

[System.Serializable]
public class License // useless i did used bcoz its necessary (for now)
{
    public string name;
    public string url;
}

[System.Serializable]
public  class DictWrapper // getting the json as list so i have made a wrapper which will be used to bypass the string with items
{
    public DictModel[] items;
}

[System.Serializable]
public class ErrorMessage // getting error message on wrong words or something .
{
    public string title;
    public string message;
}

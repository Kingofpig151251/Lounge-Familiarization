public class LanguagePlus
{
    private readonly string _traditionalChinese;
    private readonly string _english;

    public LanguagePlus(string traditionalChinese, string english)
    {
        this._traditionalChinese = traditionalChinese;
        this._english = english;
    }

    // Function to cast LanguagePlus to string using an (string)<LanguagePlus> cast
    public static implicit operator string(LanguagePlus languagePlus)
    {
        return TaskListManager.Instance.language switch
        {
            0 => languagePlus._traditionalChinese,
            1 => languagePlus._english,
            _ => "Unknown language index"
        };
    }
}
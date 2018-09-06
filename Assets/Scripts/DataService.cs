using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}

	public void CreateDB(){
		_connection.DropTable<Person> ();
		_connection.CreateTable<Person> ();

		_connection.InsertAll (new[]{
			new Person{
				Id = 1,
				Name = "Tom",
				Surname = "Perez",
				Age = 56
			},
			new Person{
				Id = 2,
				Name = "Fred",
				Surname = "Arthurson",
				Age = 16
			},
			new Person{
				Id = 3,
				Name = "John",
				Surname = "Doe",
				Age = 25
			},
			new Person{
				Id = 4,
				Name = "Roberto",
				Surname = "Huertas",
				Age = 37
			}
		});
	}

	public IEnumerable<Person> GetPersons(){
		return _connection.Table<Person>();
	}

	public IEnumerable<Person> GetPersonsNamedRoberto(){
		return _connection.Table<Person>().Where(x => x.Name == "Roberto");
	}

	public Person GetJohnny(){
		return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
	}

	public Person CreatePerson(){
		var p = new Person{
				Name = "Johnny",
				Surname = "Mnemonic",
				Age = 21
		};
		_connection.Insert (p);
		return p;
	}
    //Get Question data
    public Question TiengViet(int _id)
    {
        return _connection.Table<Question>().Where(x => x.id == _id).FirstOrDefault();
    }
    public Arap Arap(int _id)
    {
        return _connection.Table<Arap>().Where(x => x._id == _id).FirstOrDefault();
    }
    public English TiengAnh(int _id)
    {
        return _connection.Table<English>().Where(x => x._id == _id).FirstOrDefault();
    }
    public Duc TiengDuc(int _id)
    {
        return _connection.Table<Duc>().Where(x => x._id == _id).FirstOrDefault();
    }
    public TayBanNha TiengTBN(int _id)
    {
        return _connection.Table<TayBanNha>().Where(x => x._id == _id).FirstOrDefault();
    }
    public Nga TiengNga(int _id)
    {
        return _connection.Table<Nga>().Where(x => x._id == _id).FirstOrDefault();
    }
    public Phap TiengPhap(int _id)
    {
        return _connection.Table<Phap>().Where(x => x._id == _id).FirstOrDefault();
    }
    public italia TiengY(int _id)
    {
        return _connection.Table<italia>().Where(x => x._id == _id).FirstOrDefault();
    }
}
public class Question
{
    public int id { get; set; }
    public string question { get; set; }
    public string casea { get; set; }
    public string caseb { get; set; }
    public string casec { get; set; }
    public string cased { get; set; }
    public string truecase { get; set; }
}
public class Arap
{
    public int _id { get; set; }
    public string _cauHoi { get; set; }
    public string _cauA { get; set; }
    public string _cauB { get; set; }
    public string _cauC { get; set; }
    public string _cauD { get; set; }
    public int _dapAn { get; set; }
}
public class English
{
    public int _id { get; set; }
    public string _cauHoi { get; set; }
    public string _cauA { get; set; }
    public string _cauB { get; set; }
    public string _cauC { get; set; }
    public string _cauD { get; set; }
    public int _dapAn { get; set; }
}
public class Duc
{
    public int _id { get; set; }
    public string _cauHoi { get; set; }
    public string _cauA { get; set; }
    public string _cauB { get; set; }
    public string _cauC { get; set; }
    public string _cauD { get; set; }
    public int _dapAn { get; set; }
}
public class Nga
{
    public int _id { get; set; }
    public string _cauHoi { get; set; }
    public string _cauA { get; set; }
    public string _cauB { get; set; }
    public string _cauC { get; set; }
    public string _cauD { get; set; }
    public int _dapAn { get; set; }
}
public class Phap
{
    public int _id { get; set; }
    public string _cauHoi { get; set; }
    public string _cauA { get; set; }
    public string _cauB { get; set; }
    public string _cauC { get; set; }
    public string _cauD { get; set; }
    public int _dapAn { get; set; }
}
public class TayBanNha
{
    public int _id { get; set; }
    public string _cauHoi { get; set; }
    public string _cauA { get; set; }
    public string _cauB { get; set; }
    public string _cauC { get; set; }
    public string _cauD { get; set; }
    public int _dapAn { get; set; }
}
public class italia
{
    public int _id { get; set; }
    public string _cauHoi { get; set; }
    public string _cauA { get; set; }
    public string _cauB { get; set; }
    public string _cauC { get; set; }
    public string _cauD { get; set; }
    public int _dapAn { get; set; }
}


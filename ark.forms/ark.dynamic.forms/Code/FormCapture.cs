namespace ark.dynamic.forms
{
    public class FormCapture
    {
        string _con_str = "";
        public FormCapture(string event_name)
        {
            if (string.IsNullOrEmpty(event_name)) throw new ArgumentNullException("event_name");
            _con_str = connection_string(event_name);
        }
        public dynamic FetchData()
        {
            new Ark.Sqlite.SqliteManager(_con_str).CreateTable(create_table_records());
            return new Ark.Sqlite.SqliteManager(_con_str).Select(select_table_records(null));
        }
        public dynamic FetchCount()
        {
            new Ark.Sqlite.SqliteManager(_con_str).CreateTable(create_table_records());
            return new Ark.Sqlite.SqliteManager(_con_str).ExecuteCount($"select count(*) from records;");
        }
        public dynamic CaptureData(Dictionary<string, string?> props)
        {
            if (props == null || props.Count == 0) throw new ArgumentNullException("event_name");
            Dictionary<string, string?> _props = new Dictionary<string, string?>();
            props.ToList().ForEach(t => _props.Add(t.Key, Ark.Sqlite.SqliteManager.ReplaceSpecialChar(t.Value ?? "", new Dictionary<string, string?>() { { "'", "" } })));
            new Ark.Sqlite.SqliteManager(_con_str).CreateTable(create_table_records());
            long ident = (long)new Ark.Sqlite.SqliteManager(_con_str).ExecuteQuery(insert_table_records(_props));
            return new Ark.Sqlite.SqliteManager(_con_str).Select(select_table_records(ident));
        }
        Func<string, string> connection_string = (string event_name) => $"Data Source={event_name.ToLower().Trim()}.db";
        Func<string> create_table_records = () =>
        @$"CREATE TABLE  IF NOT EXISTS records (
    record_id INTEGER NOT NULL
                      CONSTRAINT PK_records PRIMARY KEY AUTOINCREMENT,
    full_name     TEXT,
    email_phone     TEXT,
    profession     TEXT,
    fellowship_src     TEXT,
    accomponied     INTEGER,
    ip        TEXT,
    at        TEXT
);
";


        Func<Dictionary<string, string?>, string> insert_table_records = (Dictionary<string, string?> props) =>
        @$"INSERT INTO records (
                        {string.Join(',', props.Keys.Select(t => $"[{t}]"))}
                    )
                    VALUES ({string.Join(',', props.Values.Select(t => $"'{t ?? ""}'"))});
";

        Func<long?, string> select_table_records = (long? record_id) =>
        @$"SELECT *
        FROM records {(record_id.HasValue && record_id.Value > 0 ? $"where record_id = {record_id}" : "")}";
    }
}

using System.Data;

class Programm
{
private void checkButton_Click(object sender, EventArgs e)
{
        if (passportTextbox.Text.Trim() == "")
    {
            int num2 = (int)MessageBox.Show("Введите серию и номер паспорта");
    }
    else
    {
            string rawData = passportTextbox.Text.Trim().Replace(" ", string.Empty);
        if (rawData.Length < 10)
        {
                textResult.Text = "Неверный формат серии или номера паспорта";
        }
        else
        {
                string commandText = string.Format($"select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(rawData));
            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
                var connection = TryConnectSQLite();
                connection.Open();
                var sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
                var dataTable = new DataTable();
                sqLiteDataAdapter.Fill(dataTable);
                SetResult(dataTable);
                connection.Close();
            }
        }
    }

    private SQLiteConnection TryConnectSQLite()
    {
        int num2;
            try
            {
            return new SQLiteConnection(connectionString);
        }
        catch (SQLiteException ex)
                {
            if (ex.ErrorCode = 1)
                num2 = (int)MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
        throw new InvalidOperationException("Не можем подсоединится к SQLite.");
                }
                else
                    this.textResult.Text = "Паспорт «" + this.passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
                connection.Close();
            }

    private void SetResult(DataTable dataTable)
    {
        if (dataTable.Rows.Count > 0)
            {
            string access = Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]) ? "ПРЕДОСТАВЛЕН" : "НЕ ПРЕДОСТАВЛЕН";
            textResult.Text = "По паспорту «" + passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании " + access;
        }
        else
            textResult.Text = "Паспорт «" + passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
    }

}
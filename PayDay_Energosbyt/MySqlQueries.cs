namespace PayDay_Energosbyt
{
    public class MySqlQueries
    {
        //Запросы вывода таблиц в DataGridView

        public string Select_Otdely = $@"SELECT ID_Otdela AS 'ID Отдела', NAME AS 'Наименование отдела' FROM otdely;";

        public string Select_Doljnosti = $@"SELECT ID_Doljnosti AS 'ID Должности', NAME AS 'Наименование должности' FROM doljnosti;";

        public string Select_Oklad = $@"SELECT oklad.ID_Oklada AS 'ID Оклада', CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) AS 'ФИО Сотрудника',
oklad.Znachenie AS 'Значение', oklad.Date_Nachala_Deistv AS 'Дата начала действия', oklad.Date_Okonchaniya_Deistv AS 'Дата окончания действия' FROM oklad LEFT JOIN sotrudniki ON oklad.ID_Oklada = sotrudniki.ID_Oklada;";

        public string Select_Oklad_Sotrudnika = $@"SELECT oklad.Znachenie FROM oklad INNER JOIN sotrudniki ON oklad.ID_Oklada = sotrudniki.ID_Oklada
WHERE sotrudniki.ID_Sotrudnika = @ID";

        public string Select_Rasch_Scheta = $@"SELECT raschetnye_scheta.ID_Rasch_scheta AS 'ID Расчетного счета', 
CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) AS 'ФИО Сотрудника',
raschetnye_scheta.Cod_strany AS 'Код страны',
raschetnye_scheta.Contr_chislo AS 'Контрольное число',
raschetnye_scheta.Balance_schet AS 'Балансовый счет',
raschetnye_scheta.Cod_banka_BIC AS 'Код банка БИК',
raschetnye_scheta.Individual_schet AS 'Индивидуальный счет'
FROM raschetnye_scheta LEFT JOIN sotrudniki ON raschetnye_scheta.ID_Rasch_scheta = sotrudniki.ID_Rasch_scheta";

        public string Select_Sotrudniki = $@"SELECT sotrudniki.ID_Sotrudnika AS 'ID Сотрудника',
CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) AS 'ФИО Сотрудника',
otdely.Name AS 'Наименование отдела', doljnosti.Name AS 'Наименование должности',
oklad.Znachenie AS 'Значение оклада',
CONCAT(raschetnye_scheta.Cod_strany,
raschetnye_scheta.Contr_chislo,
raschetnye_scheta.Balance_schet,
raschetnye_scheta.Cod_banka_BIC,
raschetnye_scheta.Individual_schet) AS 'Расчетный счет'
FROM sotrudniki 
INNER JOIN oklad ON sotrudniki.ID_Oklada = oklad.ID_Oklada 
INNER JOIN doljnosti ON sotrudniki.ID_Doljnosti = doljnosti.ID_Doljnosti 
INNER JOIN otdely ON sotrudniki.ID_Otdela = otdely.ID_Otdela
LEFT JOIN raschetnye_scheta ON sotrudniki.ID_Rasch_scheta = raschetnye_scheta.ID_Rasch_scheta";

        public string Select_Grafik_Raboty = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day),'%d %M %Y') AS 'Дата',
grafik_raboty.Identify AS 'Идентификатор', grafik_raboty.Znachenie_Raboch_Vremeni AS 'Кол-во рабочих часов'
FROM grafik_raboty INNER JOIN doljnosti ON grafik_raboty.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE doljnosti.ID_Doljnosti = @ID";

        public string Select_Grafik_Raboty_Filter = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day),'%d %M %Y') AS 'Дата начала месяца',
grafik_raboty.Identify AS 'Идентификатор', grafik_raboty.Znachenie_Raboch_Vremeni AS 'Кол-во рабочих часов'
FROM grafik_raboty INNER JOIN doljnosti ON grafik_raboty.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE doljnosti.ID_Doljnosti = @ID AND grafik_raboty.Month = @Value1 AND grafik_raboty.Year = @Value2";

        public string Select_Grafik_For_Tabel = $@"SELECT grafik_raboty.*
FROM grafik_raboty INNER JOIN doljnosti ON grafik_raboty.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE doljnosti.ID_Doljnosti = (SELECT sotrudniki.ID_Doljnosti FROM sotrudniki WHERE sotrudniki.ID_Sotrudnika = @ID) AND grafik_raboty.Month = @Value1 AND grafik_raboty.Year = @Value2";

        public string Select_Tabel = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day),'%d %M %Y') AS 'Дата',
tabel_otr_vremeni.Identify AS 'Идентификатор', tabel_otr_vremeni.Znachenie_Otr_Vremeni AS 'Кол-во отработанных часов'
FROM tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE tabel_otr_vremeni.ID_Sotrudnika = @ID";

        public string Select_Tabel_Filter = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day),'%d %M %Y') AS 'Дата',
tabel_otr_vremeni.Identify AS 'Идентификатор', tabel_otr_vremeni.Znachenie_Otr_Vremeni AS 'Кол-во отработанных часов'
FROM tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE tabel_otr_vremeni.ID_Sotrudnika = @ID AND tabel_otr_vremeni.Month = @Value1 AND tabel_otr_vremeni.Year = @Value2";

        public string Select_Vyplaty = $@"SELECT vyplaty.Id, vyplaty.Date AS 'Дата начисления', vyplaty.Date_Begin AS 'Дата начала периода начисления', 
vyplaty.Date_End AS 'Дата конца периода начисления', 
CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) AS 'ФИО Сотрудника',
vyplaty.Otrabotano AS 'Начислено зар. платы', vyplaty.Uderzhaniya AS 'Удержано из зар. платы',
vyplaty.Itog AS 'Итого'
FROM vyplaty INNER JOIN sotrudniki ON vyplaty.ID_Sotrudnika = sotrudniki.ID_Sotrudnika";

//        public string Select_Otrabotano = $@"SELECT Sum(tabel_otr_vremeni.Znachenie_Otr_Vremeni) FROM
//tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
//WHERE CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day) BETWEEN @Value1 AND @Value2
//AND sotrudniki.ID_Sotrudnika = @ID";

//        public string Select_Plan = $@"SELECT Sum(grafik_raboty.Znachenie_Raboch_Vremeni) FROM
//grafik_raboty INNER JOIN sotrudniki ON grafik_raboty.ID_Doljnosti = sotrudniki.ID_Doljnosti
//WHERE CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day) BETWEEN @Value1 AND @Value2
//AND sotrudniki.ID_Sotrudnika = @ID";

//        public string Select_Stavka = $@"SELECT oklad.Znachenie / @Value1
//FROM oklad
//INNER JOIN sotrudniki ON oklad.ID_Oklada = sotrudniki.ID_Oklada
//WHERE sotrudniki.ID_Sotrudnika = @ID";

//        public string Select_Nachisleno = $@"SELECT DISTINCT @Value1 * @Value2
//FROM tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
//WHERE sotrudniki.ID_Sotrudnika = @ID";

        public string Select_Nachisleno = $@"SELECT DISTINCT ROUND((SELECT oklad.Znachenie / (SELECT Sum(grafik_raboty.Znachenie_Raboch_Vremeni) FROM
grafik_raboty INNER JOIN sotrudniki ON grafik_raboty.ID_Doljnosti = sotrudniki.ID_Doljnosti
WHERE CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day) BETWEEN @Value1 AND @Value2
AND sotrudniki.ID_Sotrudnika = @ID)
FROM oklad
INNER JOIN sotrudniki ON oklad.ID_Oklada = sotrudniki.ID_Oklada
WHERE sotrudniki.ID_Sotrudnika = @ID) * (SELECT Sum(tabel_otr_vremeni.Znachenie_Otr_Vremeni) FROM
tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day) BETWEEN @Value1 AND @Value2
AND sotrudniki.ID_Sotrudnika = @ID),2)
FROM tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE sotrudniki.ID_Sotrudnika = @ID";
        //Запросы вывода таблиц в DataGridView

        //Запросы вывода данный в ComboBox

        public string Select_Sotrudniki_ComboBox = $@"SELECT CONCAT(Familiya, ' ', Imya, ' ', Otchestvo) AS 'ФИО Сотрудника'
FROM sotrudniki";

        public string Select_Otdely_ComboBox = $@"SELECT Name AS 'Наименование отдела'
FROM otdely";

        public string Select_Oklad_ComboBox = $@"SELECT Znachenie AS 'Значение оклада'
FROM oklad";

        public string Select_Doljnosti_ComboBox = $@"SELECT Name AS 'Наименование должности'
FROM doljnosti";

        public string Select_Rasch_Scheta_ComboBox = $@"SELECT CONCAT(raschetnye_scheta.Cod_strany,
raschetnye_scheta.Contr_chislo,
raschetnye_scheta.Balance_schet,
raschetnye_scheta.Cod_banka_BIC,
raschetnye_scheta.Individual_schet) AS 'Расчетный счет'
FROM raschetnye_scheta LEFT JOIN sotrudniki ON sotrudniki.ID_Rasch_scheta = raschetnye_scheta.ID_Rasch_scheta
WHERE CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) IS NULL";

        //Запросы вывода данный в ComboBox

        //Запросы получения ID из значений ComboBox

        public string Select_ID_Otdela = $@"SELECT otdely.ID_Otdela FROM otdely WHERE otdely.Name = @Value1";

        public string Select_ID_Doljnosti = $@"SELECT doljnosti.ID_Doljnosti FROM doljnosti WHERE doljnosti.Name = @Value1";

        public string Select_ID_Oklada = $@"SELECT oklad.ID_Oklada FROM oklad WHERE oklad.Znachenie = @Value1";

        public string Select_ID_Rasch_Scheta = $@"SELECT raschetnye_scheta.ID_Rasch_scheta FROM raschetnye_scheta 
WHERE CONCAT(raschetnye_scheta.Cod_strany,
raschetnye_scheta.Contr_chislo,
raschetnye_scheta.Balance_schet,
raschetnye_scheta.Cod_banka_BIC,
raschetnye_scheta.Individual_schet) = @Value1";

        public string Select_ID_Sotrudnika = $@"SELECT sotrudniki.ID_Sotrudnika FROM sotrudniki
WHERE CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya, ' ',sotrudniki.Otchestvo) = @Value1";

        //Запросы получения ID из значений ComboBox

        //Запросы вставки в таблицы

        public string Insert_Doljnosti = $@"INSERT INTO doljnosti (Name) VALUES (@Value1);";

        public string Insert_Otdely = $@"INSERT INTO otdely (Name) VALUES (@Value1);";

        public string Insert_Rasch_Scheta = $@"INSERT INTO raschetnye_scheta (Cod_strany, Contr_chislo, Balance_schet, Cod_banka_BIC, Individual_schet) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Oklad = $@"INSERT INTO oklad (Znachenie, Date_Nachala_Deistv, Date_Okonchaniya_Deistv) VALUES (@Value1, @Value2, @Value3);";

        public string Insert_Sotrudniki = $@"INSERT INTO sotrudniki (Familiya, Imya, Otchestvo, ID_Otdela, ID_Doljnosti, ID_Oklada, ID_Rasch_scheta) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7);";

        public string Insert_Grafik_Raboty = $@"INSERT INTO grafik_raboty (ID_Doljnosti, Year, Month, Day, Identify, Znachenie_Raboch_Vremeni) VALUES (@ID, @Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Tabel = $@"INSERT INTO tabel_otr_vremeni (ID_Sotrudnika, Year, Month, Day, Identify, Znachenie_Otr_Vremeni) VALUES (@ID, @Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Vyplaty = $@"INSERT INTO vyplaty (Date, Date_Begin, Date_End, ID_Sotrudnika, Otrabotano, Uderzhaniya, Itog) VALUES (@Value1, @Value2, @Value3, @ID, @Value4, @Value5, @Value6);";

        //Запросы вставки в таблицы


        //Запросы редактирования записей

        public string Update_Doljnosti = $@"UPDATE doljnosti SET Name = @Value1 WHERE ID_Doljnosti = @ID;";

        public string Update_Otdely = $@"UPDATE otdely SET Name = @Value1 WHERE ID_Otdela = @ID;";

        public string Update_Rasch_Scheta = $@"UPDATE raschetnye_scheta SET Cod_strany=@Value1, Contr_chislo=@Value2, Balance_schet=@Value3, Cod_banka_BIC=@Value4, Individual_schet=@Value5 WHERE ID_Rasch_scheta=@ID;";

        public string Update_Oklad = $@"UPDATE oklad SET Znachenie = @Value1, Date_Nachala_Deistv = @Value2, Date_Okonchaniya_Deistv = @Value3 WHERE ID_Oklada = @ID;";
        
        public string Update_Sotrudniki = $@"UPDATE sotrudniki SET Familiya = @Value1, Imya = @Value2, 
Otchestvo = @Value3, ID_Otdela = @Value4, ID_Doljnosti = @Value5, ID_Oklada = @Value6, 
ID_Rasch_Scheta = @Value7
WHERE ID_Sotrudnika = @ID";

        public string Update_Grafik_Raboty = $@"UPDATE grafik_raboty 
SET grafik_raboty.Znachenie_Raboch_Vremeni = @Value2, grafik_raboty.Identify = @Value1 
WHERE DATE_FORMAT(CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day),'%d %M %Y') = @Value3
AND grafik_raboty.ID_Doljnosti = @ID";

        public string Update_Tabel = $@"UPDATE tabel_otr_vremeni 
SET tabel_otr_vremeni.Znachenie_Otr_Vremeni = @Value2, tabel_otr_vremeni.Identify = @Value1 
WHERE DATE_FORMAT(CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day),'%d %M %Y') = @Value3
AND tabel_otr_vremeni.ID_Sotrudnika = @ID";
        //Запросы редактирования записей


        //Запросы удаления записей

        public string Delete_Doljnosti = $@"DELETE FROM doljnosti WHERE ID_Doljnosti = @ID;";

        public string Delete_Otdely = $@"DELETE FROM otdely WHERE ID_Otdela = @ID;";

        public string Delete_Rasch_Scheta = $@"DELETE FROM raschetnye_scheta WHERE ID_Rasch_scheta = @ID;";

        public string Delete_Oklad = $@"DELETE FROM oklad WHERE ID_Oklada = @ID";

        public string Delete_Sotrudniki = $@"DELETE FROM sotrudniki WHERE ID_Sotrudnika = @ID";

        public string Delete_Vyplaty = $@"DELETE FROM vyplaty WHERE Id= @ID;";

        //Запросы удаления записей

        //Запросы проверки на существование

        public string Exists_Grafik_Raboty = $@"SELECT EXISTS (SELECT * FROM grafik_raboty WHERE grafik_raboty.ID_Doljnosti = @ID AND grafik_raboty.Year = @Value1 AND grafik_raboty.Month = @Value2 AND grafik_raboty.Day = @Value3)";

        public string Exists_Tabel = $@"SELECT EXISTS (SELECT * FROM tabel_otr_vremeni WHERE tabel_otr_vremeni.ID_Sotrudnika = @ID AND tabel_otr_vremeni.Year = @Value1 AND tabel_otr_vremeni.Month = @Value2 AND tabel_otr_vremeni.Day = @Value3)";

        public string Exists_Rasch_Scheta = $@"SELECT EXISTS(SELECT * FROM raschetnye_scheta WHERE CONCAT(raschetnye_scheta.Cod_strany,raschetnye_scheta.Contr_chislo,raschetnye_scheta.Balance_schet,raschetnye_scheta.Cod_banka_BIC,raschetnye_scheta.Individual_schet) = @Value1)";

        public string Exists_Otdely = $@"SELECT EXISTS(SELECT * FROM otdely WHERE otdely.Name = @Value1)";

        public string Exists_Doljnosti = $@"SELECT EXISTS(SELECT * FROM doljnosti WHERE doljnosti.Name = @Value1)";

        public string Exists_Oklad = $@"SELECT EXISTS(SELECT * FROM oklad WHERE oklad.Znachenie = @Value1 AND oklad.Date_Nachala_Deistv = @Value2 AND oklad.Date_Okonchaniya_Deistv = @Value3)";

        public string Exists_Vyplaty = $@"SELECT EXISTS(SELECT * FROM vyplaty WHERE vyplaty.Date_Begin = @Value1 AND vyplaty.Date_Begin = @Value2 AND vyplaty.Date_End = @Value3 AND vyplaty.ID_Sotrudnika = @ID)";

        //Запросы проверки на существование
    }
}

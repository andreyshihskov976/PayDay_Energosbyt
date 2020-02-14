namespace PayDay_Energosbyt
{
    public class MySqlQueries
    {
        //Запросы Select

        public string Select_Otdely = $@"SELECT ID_Otdela AS 'ID Отдела', NAME AS 'Наименование отдела' FROM otdely;";

        public string Select_Doljnosti = $@"SELECT ID_Doljnosti AS 'ID Должности', NAME AS 'Наименование должности' FROM doljnosti;";

        public string Select_Doljnost_by_ID = $@"SELECT NAME AS 'Наименование должности' FROM doljnosti WHERE ID_Doljnosti = @ID;";

        public string Select_Otdel_by_ID = $@"SELECT otdely.Name FROM otdely WHERE otdely.ID_Otdela = @ID;";

        public string Select_Oklad = $@"SELECT oklad.ID_Oklada AS 'ID Оклада', CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) AS 'ФИО Сотрудника',
oklad.Znachenie AS 'Значение', oklad.Date_Nachala_Deistv AS 'Дата начала действия', oklad.Date_Okonchaniya_Deistv AS 'Дата окончания действия' FROM oklad LEFT JOIN sotrudniki ON oklad.ID_Oklada = sotrudniki.ID_Oklada;";

        public string Select_Oklad_Sotrudnika = $@"SELECT oklad.Znachenie FROM oklad INNER JOIN sotrudniki ON oklad.ID_Oklada = sotrudniki.ID_Oklada
WHERE sotrudniki.ID_Sotrudnika = @ID;";

        public string Select_Rasch_Scheta = $@"SELECT raschetnye_scheta.ID_Rasch_scheta AS 'ID Расчетного счета', 
CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) AS 'ФИО Сотрудника',
raschetnye_scheta.Cod_strany AS 'Код страны',
raschetnye_scheta.Contr_chislo AS 'Контрольное число',
raschetnye_scheta.Balance_schet AS 'Балансовый счет',
raschetnye_scheta.Cod_banka_BIC AS 'Код банка БИК',
raschetnye_scheta.Individual_schet AS 'Индивидуальный счет'
FROM raschetnye_scheta LEFT JOIN sotrudniki ON raschetnye_scheta.ID_Rasch_scheta = sotrudniki.ID_Rasch_scheta;";

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
LEFT JOIN oklad ON sotrudniki.ID_Oklada = oklad.ID_Oklada 
LEFT JOIN doljnosti ON sotrudniki.ID_Doljnosti = doljnosti.ID_Doljnosti 
LEFT JOIN otdely ON sotrudniki.ID_Otdela = otdely.ID_Otdela
LEFT JOIN raschetnye_scheta ON sotrudniki.ID_Rasch_scheta = raschetnye_scheta.ID_Rasch_scheta;";

        public string Select_FIO_Sotrudnika_by_ID = $@"SELECT CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya, ' ',sotrudniki.Otchestvo)
FROM sotrudniki
WHERE sotrudniki.ID_Sotrudnika = @ID;";

        public string Select_Otdel_Sotrudnika_by_ID = $@"SELECT CONCAT(sotrudniki.ID_Otdela, ' ', otdely.Name)
FROM sotrudniki INNER JOIN otdely ON sotrudniki.ID_Otdela = otdely.ID_Otdela
WHERE sotrudniki.ID_Sotrudnika = @ID;";

        public string Select_Doljnost_Sotrudnika_by_ID = $@"SELECT doljnosti.Name
FROM sotrudniki INNER JOIN doljnosti ON sotrudniki.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE sotrudniki.ID_Sotrudnika = @ID;";

        public string Select_Grafik_Raboty = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day),'%d %M %Y') AS 'Дата',
grafik_raboty.Identify AS 'Идентификатор', grafik_raboty.Znachenie_Raboch_Vremeni AS 'Кол-во рабочих часов'
FROM grafik_raboty INNER JOIN doljnosti ON grafik_raboty.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE doljnosti.ID_Doljnosti = @ID;";

        public string Select_Grafik_Raboty_Name = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day),'%d %M %Y') AS 'Дата',
grafik_raboty.Identify AS 'Идентификатор', grafik_raboty.Znachenie_Raboch_Vremeni AS 'Кол-во рабочих часов'
FROM grafik_raboty INNER JOIN doljnosti ON grafik_raboty.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE doljnosti.Name = @ID;";

        public string Select_Grafik_Raboty_FIO = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day),'%d %M %Y') AS 'Дата',
grafik_raboty.Identify AS 'Идентификатор', grafik_raboty.Znachenie_Raboch_Vremeni AS 'Кол-во рабочих часов'
FROM grafik_raboty INNER JOIN doljnosti ON grafik_raboty.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE doljnosti.ID_Doljnosti = (SELECT sotrudniki.ID_Doljnosti FROM sotrudniki 
WHERE CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) = @ID);";

        public string Select_Grafik_Raboty_Filter = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day),'%d %M %Y') AS 'Дата начала месяца',
grafik_raboty.Identify AS 'Идентификатор', grafik_raboty.Znachenie_Raboch_Vremeni AS 'Кол-во рабочих часов'
FROM grafik_raboty INNER JOIN doljnosti ON grafik_raboty.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE doljnosti.ID_Doljnosti = @ID AND grafik_raboty.Month = @Value1 AND grafik_raboty.Year = @Value2;";

        public string Select_Grafik_For_Tabel = $@"SELECT grafik_raboty.*
FROM grafik_raboty INNER JOIN doljnosti ON grafik_raboty.ID_Doljnosti = doljnosti.ID_Doljnosti
WHERE doljnosti.ID_Doljnosti = (SELECT sotrudniki.ID_Doljnosti FROM sotrudniki WHERE sotrudniki.ID_Sotrudnika = @ID) AND grafik_raboty.Year = @Value1;";

        public string Select_Tabel = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day),'%d %M %Y') AS 'Дата',
tabel_otr_vremeni.Identify AS 'Идентификатор', tabel_otr_vremeni.Znachenie_Otr_Vremeni AS 'Кол-во отработанных часов'
FROM tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE tabel_otr_vremeni.ID_Sotrudnika = @ID;";

        public string Select_Tabel_FIO = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day),'%d %M %Y') AS 'Дата',
tabel_otr_vremeni.Identify AS 'Идентификатор', tabel_otr_vremeni.Znachenie_Otr_Vremeni AS 'Кол-во отработанных часов'
FROM tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE tabel_otr_vremeni.ID_Sotrudnika = (SELECT sotrudniki.ID_Sotrudnika FROM sotrudniki
WHERE CONCAT(sotrudniki.Familiya,' ',sotrudniki.Imya,' ',sotrudniki.Otchestvo) = @ID);";

        public string Select_Tabel_Filter = $@"SET lc_time_names = 'ru_RU'; SELECT
DATE_FORMAT(CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day),'%d %M %Y') AS 'Дата',
tabel_otr_vremeni.Identify AS 'Идентификатор', tabel_otr_vremeni.Znachenie_Otr_Vremeni AS 'Кол-во отработанных часов'
FROM tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE tabel_otr_vremeni.ID_Sotrudnika = @ID AND tabel_otr_vremeni.Month = @Value1 AND tabel_otr_vremeni.Year = @Value2;";

        public string Select_Vyplaty = $@"SELECT vyplaty.Id, vyplaty.Date AS 'Дата начисления', vyplaty.Date_Begin AS 'Дата начала периода начисления', 
vyplaty.Date_End AS 'Дата конца периода начисления', 
CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) AS 'ФИО Сотрудника',
vyplaty.Otrabotano AS 'Начислено зар. платы', vyplaty.Uderzhaniya AS 'Удержано из зар. платы',
vyplaty.Itog AS 'Итого'
FROM vyplaty INNER JOIN sotrudniki ON vyplaty.ID_Sotrudnika = sotrudniki.ID_Sotrudnika;";

        public string Select_Otrabotano = $@"SELECT SUM(tabel_otr_vremeni.Znachenie_Otr_Vremeni) FROM
tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE STR_TO_DATE(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya, ' ',sotrudniki.Otchestvo) = @ID";

        public string Select_Kol_Dney_Otrabotano = $@"SELECT COUNT(tabel_otr_vremeni.Znachenie_Otr_Vremeni) FROM
tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE STR_TO_DATE(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya, ' ',sotrudniki.Otchestvo) = @ID";

        public string Select_Nachisleno = $@"SELECT DISTINCT ROUND((SELECT oklad.Znachenie / (SELECT Sum(grafik_raboty.Znachenie_Raboch_Vremeni) FROM
grafik_raboty INNER JOIN sotrudniki ON grafik_raboty.ID_Doljnosti = sotrudniki.ID_Doljnosti
WHERE STR_TO_DATE(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND sotrudniki.ID_Sotrudnika = @ID)
FROM oklad
INNER JOIN sotrudniki ON oklad.ID_Oklada = sotrudniki.ID_Oklada
WHERE sotrudniki.ID_Sotrudnika = @ID) * (SELECT Sum(tabel_otr_vremeni.Znachenie_Otr_Vremeni) FROM
tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE STR_TO_DATE(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND sotrudniki.ID_Sotrudnika = @ID),2)
FROM tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE sotrudniki.ID_Sotrudnika = @ID;";

        public string Select_Sotrudniki_ComboBox = $@"SELECT CONCAT(Familiya, ' ', Imya, ' ', Otchestvo) AS 'ФИО Сотрудника'
FROM sotrudniki;";

        public string Select_Otdely_ComboBox = $@"SELECT Name AS 'Наименование отдела'
FROM otdely;";

        public string Select_Oklad_ComboBox = $@"SELECT Znachenie AS 'Значение оклада'
FROM oklad;";

        public string Select_Doljnosti_ComboBox = $@"SELECT Name AS 'Наименование должности'
FROM doljnosti;";

        public string Select_Rasch_Scheta_ComboBox = $@"SELECT CONCAT(raschetnye_scheta.Cod_strany,
raschetnye_scheta.Contr_chislo,
raschetnye_scheta.Balance_schet,
raschetnye_scheta.Cod_banka_BIC,
raschetnye_scheta.Individual_schet) AS 'Расчетный счет'
FROM raschetnye_scheta LEFT JOIN sotrudniki ON sotrudniki.ID_Rasch_scheta = raschetnye_scheta.ID_Rasch_scheta
WHERE CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) IS NULL;";

        public string Select_ID_Otdela = $@"SELECT otdely.ID_Otdela FROM otdely WHERE otdely.Name = @Value1;";

        public string Select_ID_Doljnosti = $@"SELECT doljnosti.ID_Doljnosti FROM doljnosti WHERE doljnosti.Name = @Value1;";

        public string Select_ID_Doljnosti_Sotrudnika = $@"SELECT doljnosti.ID_Doljnosti FROM doljnosti INNER JOIN sotrudniki
ON doljnosti.ID_Doljnosti = sotrudniki.ID_Doljnosti 
WHERE sotrudniki.ID_Sotrudnika = (SELECT sotrudniki.ID_Sotrudnika FROM sotrudniki
WHERE CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya, ' ',sotrudniki.Otchestvo) = @Value1);";

        public string Select_ID_Oklada = $@"SELECT oklad.ID_Oklada FROM oklad WHERE oklad.Znachenie = @Value1;";

        public string Select_ID_Rasch_Scheta = $@"SELECT raschetnye_scheta.ID_Rasch_scheta FROM raschetnye_scheta 
WHERE CONCAT(raschetnye_scheta.Cod_strany,
raschetnye_scheta.Contr_chislo,
raschetnye_scheta.Balance_schet,
raschetnye_scheta.Cod_banka_BIC,
raschetnye_scheta.Individual_schet) = @Value1;";

        public string Select_ID_Sotrudnika = $@"SELECT sotrudniki.ID_Sotrudnika FROM sotrudniki
WHERE CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya, ' ',sotrudniki.Otchestvo) = @Value1;";

        public string Select_Kol_Rab_Dney_Grafik = $@"SELECT
COUNT(grafik_raboty.Identify)
FROM grafik_raboty
WHERE grafik_raboty.Identify = 'Р' AND STR_TO_DATE(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND grafik_raboty.ID_Doljnosti = @ID;";

        public string Select_Kol_PredPrazdn_Dney_Grafik = $@"SELECT
COUNT(grafik_raboty.Identify)
FROM grafik_raboty
WHERE grafik_raboty.Identify = 'Р' AND grafik_raboty.Znachenie_Raboch_Vremeni = '7.2' AND STR_TO_DATE(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND grafik_raboty.ID_Doljnosti = @ID;";

        public string Select_Kol_Poln_Dney_Grafik = $@"SELECT
COUNT(grafik_raboty.Identify)
FROM grafik_raboty
WHERE grafik_raboty.Identify = 'Р' AND grafik_raboty.Znachenie_Raboch_Vremeni = '8.2' AND STR_TO_DATE(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND grafik_raboty.ID_Doljnosti = @ID;";

        public string Select_Itogo_Rab_Chasov_Grafik = $@"SELECT
SUM(grafik_raboty.Znachenie_Raboch_Vremeni)
FROM grafik_raboty
WHERE STR_TO_DATE(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d') 
AND grafik_raboty.ID_Doljnosti = @ID;";

        public string Select_Kol_Rab_Dney_Tabel = $@"SELECT
COUNT(tabel_otr_vremeni.Identify)
FROM tabel_otr_vremeni
WHERE tabel_otr_vremeni.Identify = 'Р' AND STR_TO_DATE(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d') 
AND tabel_otr_vremeni.ID_Sotrudnika = @ID
GROUP BY tabel_otr_vremeni.Month;";

        public string Select_VP_Tabel = $@"SELECT
COUNT(tabel_otr_vremeni.Identify)
FROM tabel_otr_vremeni
WHERE (tabel_otr_vremeni.Identify = 'П' OR tabel_otr_vremeni.Identify = 'В') AND STR_TO_DATE(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND tabel_otr_vremeni.ID_Sotrudnika = @ID
GROUP BY tabel_otr_vremeni.Month;";

        public string Select_Otrabotano_Tabel = $@"SELECT Sum(tabel_otr_vremeni.Znachenie_Otr_Vremeni) FROM
tabel_otr_vremeni INNER JOIN sotrudniki ON tabel_otr_vremeni.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
WHERE STR_TO_DATE(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND sotrudniki.ID_Sotrudnika = @ID
GROUP BY tabel_otr_vremeni.Month;";

        public string Select_Vyplaty_Otdela = $@"SELECT vyplaty.Id, CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo), vyplaty.Otrabotano,vyplaty.Uderzhaniya,vyplaty.Itog
FROM vyplaty
INNER JOIN sotrudniki ON vyplaty.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
INNER JOIN otdely ON sotrudniki.ID_Otdela = otdely.ID_Otdela
WHERE otdely.ID_Otdela = @ID AND vyplaty.Date BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d');";

        public string Itog_Vyplat_Po_Otdelu = $@"SELECT SUM(vyplaty.Itog)
FROM vyplaty
INNER JOIN sotrudniki ON vyplaty.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
INNER JOIN otdely ON sotrudniki.ID_Otdela = otdely.ID_Otdela
WHERE otdely.ID_Otdela = @ID;";

        //Запросы Select



        //Запросы Insert

        public string Insert_Doljnosti = $@"INSERT INTO doljnosti (Name) VALUES (@Value1);";

        public string Insert_Otdely = $@"INSERT INTO otdely (Name) VALUES (@Value1);";

        public string Insert_Rasch_Scheta = $@"INSERT INTO raschetnye_scheta (Cod_strany, Contr_chislo, Balance_schet, Cod_banka_BIC, Individual_schet) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Oklad = $@"INSERT INTO oklad (Znachenie, Date_Nachala_Deistv, Date_Okonchaniya_Deistv) VALUES (@Value1, @Value2, @Value3);";

        public string Insert_Sotrudniki = $@"INSERT INTO sotrudniki (Familiya, Imya, Otchestvo, ID_Otdela, ID_Doljnosti, ID_Oklada, ID_Rasch_scheta) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7);";

        public string Insert_Grafik_Raboty = $@"INSERT INTO grafik_raboty (ID_Doljnosti, Year, Month, Day, Identify, Znachenie_Raboch_Vremeni) VALUES (@ID, @Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Tabel = $@"INSERT INTO tabel_otr_vremeni (ID_Sotrudnika, Year, Month, Day, Identify, Znachenie_Otr_Vremeni) VALUES (@ID, @Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Vyplaty = $@"INSERT INTO vyplaty (Date, Date_Begin, Date_End, ID_Sotrudnika, Otrabotano, Uderzhaniya, Itog) VALUES (@Value1, @Value2, @Value3, @ID, @Value4, @Value5, @Value6);";

        //Запросы Insert



        //Запросы Update

        public string Update_Doljnosti = $@"UPDATE doljnosti SET Name = @Value1 WHERE ID_Doljnosti = @ID;";

        public string Update_Otdely = $@"UPDATE otdely SET Name = @Value1 WHERE ID_Otdela = @ID;";

        public string Update_Rasch_Scheta = $@"UPDATE raschetnye_scheta SET Cod_strany=@Value1, Contr_chislo=@Value2, Balance_schet=@Value3, Cod_banka_BIC=@Value4, Individual_schet=@Value5 WHERE ID_Rasch_scheta=@ID;";

        public string Update_Oklad = $@"UPDATE oklad SET Znachenie = @Value1, Date_Nachala_Deistv = @Value2, Date_Okonchaniya_Deistv = @Value3 WHERE ID_Oklada = @ID;";
        
        public string Update_Sotrudniki = $@"UPDATE sotrudniki SET Familiya = @Value1, Imya = @Value2, 
Otchestvo = @Value3, ID_Otdela = @Value4, ID_Doljnosti = @Value5, ID_Oklada = @Value6, 
ID_Rasch_Scheta = @Value7
WHERE ID_Sotrudnika = @ID;";

        public string Update_Grafik_Raboty = $@"UPDATE grafik_raboty 
SET grafik_raboty.Znachenie_Raboch_Vremeni = @Value2, grafik_raboty.Identify = @Value1 
WHERE DATE_FORMAT(CONCAT(grafik_raboty.Year, '-',grafik_raboty.Month, '-',grafik_raboty.Day),'%d %M %Y') = @Value3
AND grafik_raboty.ID_Doljnosti = @ID;";

        public string Update_Tabel = $@"UPDATE tabel_otr_vremeni 
SET tabel_otr_vremeni.Znachenie_Otr_Vremeni = @Value2, tabel_otr_vremeni.Identify = @Value1 
WHERE DATE_FORMAT(CONCAT(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-',tabel_otr_vremeni.Day),'%d %M %Y') = @Value3
AND tabel_otr_vremeni.ID_Sotrudnika = @ID;";
        
        //Запросы Update



        //Запросы Delete

        public string Delete_Doljnosti = $@"DELETE FROM doljnosti WHERE ID_Doljnosti = @ID;";

        public string Delete_Otdely = $@"DELETE FROM otdely WHERE ID_Otdela = @ID;";

        public string Delete_Rasch_Scheta = $@"DELETE FROM raschetnye_scheta WHERE ID_Rasch_scheta = @ID;";

        public string Delete_Oklad = $@"DELETE FROM oklad WHERE ID_Oklada = @ID;";

        public string Delete_Sotrudniki = $@"DELETE FROM sotrudniki WHERE ID_Sotrudnika = @ID;";

        public string Delete_Vyplaty = $@"DELETE FROM vyplaty WHERE Id= @ID;";

        //Запросы Delete



        //Запросы Exists

        public string Exists_Grafik_Raboty = $@"SELECT EXISTS (SELECT * FROM grafik_raboty WHERE grafik_raboty.ID_Doljnosti = @ID AND grafik_raboty.Year = @Value1 AND grafik_raboty.Month = @Value2 AND grafik_raboty.Day = @Value3);";

        public string Exists_Tabel = $@"SELECT EXISTS (SELECT * FROM tabel_otr_vremeni WHERE tabel_otr_vremeni.ID_Sotrudnika = @ID AND tabel_otr_vremeni.Year = @Value1 AND tabel_otr_vremeni.Month = @Value2 AND tabel_otr_vremeni.Day = @Value3);";

        public string Exists_Rasch_Scheta = $@"SELECT EXISTS(SELECT * FROM raschetnye_scheta WHERE CONCAT(raschetnye_scheta.Cod_strany,raschetnye_scheta.Contr_chislo,raschetnye_scheta.Balance_schet,raschetnye_scheta.Cod_banka_BIC,raschetnye_scheta.Individual_schet) = @Value1);";

        public string Exists_Otdely = $@"SELECT EXISTS(SELECT * FROM otdely WHERE otdely.Name = @Value1);";

        public string Exists_Doljnosti = $@"SELECT EXISTS(SELECT * FROM doljnosti WHERE doljnosti.Name = @Value1);";

        public string Exists_Oklad = $@"SELECT EXISTS(SELECT * FROM oklad WHERE oklad.Znachenie = @Value1 AND oklad.Date_Nachala_Deistv = @Value2 AND oklad.Date_Okonchaniya_Deistv = @Value3);";

        public string Exists_Vyplaty = $@"SELECT EXISTS(SELECT * FROM vyplaty WHERE vyplaty.Date_Begin = @Value1 AND vyplaty.Date_Begin = @Value2 AND vyplaty.Date_End = @Value3 AND vyplaty.ID_Sotrudnika = @ID);";

        public string Exists_Grafik_Raboty_Print = $@"SELECT EXISTS
(SELECT * FROM grafik_raboty
WHERE STR_TO_DATE(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND grafik_raboty.ID_Doljnosti = @ID);";

        public string Exists_Grafik_Raboty_Vylaty = $@"SELECT EXISTS
(SELECT * FROM grafik_raboty
WHERE STR_TO_DATE(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND grafik_raboty.ID_Doljnosti = (SELECT sotrudniki.ID_Doljnosti FROM sotrudniki WHERE sotrudniki.ID_Sotrudnika = @ID));";

        public string Exists_Tabel_Print = $@"SELECT EXISTS
(SELECT * FROM tabel_otr_vremeni
WHERE STR_TO_DATE(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND tabel_otr_vremeni.ID_Sotrudnika = @ID);";

        //Запросы Exists



        //Запросы Print

        public string Print_Grafik = $@"SET lc_time_names = 'ru_RU';
SELECT
  Date_Format(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%M') AS 'Месяц',
  		Max(CASE grafik_raboty.Day
        WHEN 1 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '1',
      Max(CASE grafik_raboty.Day
        WHEN 2 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '2',
      Max(CASE grafik_raboty.Day
        WHEN 3 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '3',
      Max(CASE grafik_raboty.Day
        WHEN 4 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '4',
      Max(CASE grafik_raboty.Day
        WHEN 5 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '5',
      Max(CASE grafik_raboty.Day
        WHEN 6 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '6',
      Max(CASE grafik_raboty.Day
        WHEN 7 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '7',
      Max(CASE grafik_raboty.Day
        WHEN 8 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '8',
      Max(CASE grafik_raboty.Day
        WHEN 9 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '9',
      Max(CASE grafik_raboty.Day
        WHEN 10 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '10',
      Max(CASE grafik_raboty.Day
        WHEN 11 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '11',
      Max(CASE grafik_raboty.Day
        WHEN 12 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '12',
      Max(CASE grafik_raboty.Day
        WHEN 13 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '13',
      Max(CASE grafik_raboty.Day
        WHEN 14 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '14',
      Max(CASE grafik_raboty.Day
        WHEN 15 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '15',
      Max(CASE grafik_raboty.Day
        WHEN 16 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '16',
      Max(CASE grafik_raboty.Day
        WHEN 17 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '17',
      Max(CASE grafik_raboty.Day
        WHEN 18 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '18',
      Max(CASE grafik_raboty.Day
        WHEN 19 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '19',
      Max(CASE grafik_raboty.Day
        WHEN 20 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '20',
      Max(CASE grafik_raboty.Day
        WHEN 21 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '21',
      Max(CASE grafik_raboty.Day
        WHEN 22 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '22',
      Max(CASE grafik_raboty.Day
        WHEN 23 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '23',
      Max(CASE grafik_raboty.Day
        WHEN 24 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '24',
      Max(CASE grafik_raboty.Day
        WHEN 25 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '25',
      Max(CASE grafik_raboty.Day
        WHEN 26 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '26',
      Max(CASE grafik_raboty.Day
        WHEN 27 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '27',
      Max(CASE grafik_raboty.Day
        WHEN 28 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '28',
      Max(CASE grafik_raboty.Day
        WHEN 29 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '29',
      Max(CASE grafik_raboty.Day
        WHEN 30 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '30',
      Max(CASE grafik_raboty.Day
        WHEN 31 THEN (CASE grafik_raboty.Znachenie_Raboch_Vremeni WHEN 0.00 THEN grafik_raboty.Identify ELSE grafik_raboty.Znachenie_Raboch_Vremeni END)
        ELSE null
      END) AS '31'
FROM grafik_raboty
WHERE STR_TO_DATE(Concat(grafik_raboty.Year, '-',grafik_raboty.Month, '-', grafik_raboty.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND grafik_raboty.ID_Doljnosti = @ID
GROUP BY grafik_raboty.Month
ORDER BY 'Месяц';";

        public string Print_Tabel = $@"SET lc_time_names = 'ru_RU';
SELECT
  Date_Format(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%M') AS 'Месяц',
  		Max(CASE tabel_otr_vremeni.Day
        WHEN 1 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '1',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 2 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '2',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 3 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '3',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 4 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '4',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 5 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '5',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 6 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '6',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 7 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '7',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 8 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '8',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 9 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '9',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 10 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '10',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 11 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '11',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 12 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '12',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 13 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '13',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 14 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '14',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 15 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '15',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 16 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '16',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 17 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '17',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 18 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '18',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 19 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '19',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 20 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '20',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 21 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '21',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 22 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '22',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 23 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '23',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 24 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '24',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 25 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '25',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 26 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '26',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 27 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '27',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 28 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '28',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 29 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '29',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 30 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '30',
      Max(CASE tabel_otr_vremeni.Day
        WHEN 31 THEN (CASE tabel_otr_vremeni.Znachenie_Otr_Vremeni WHEN 0.00 THEN tabel_otr_vremeni.Identify ELSE tabel_otr_vremeni.Znachenie_Otr_Vremeni END)
        ELSE null
      END) AS '31'
FROM tabel_otr_vremeni
WHERE STR_TO_DATE(Concat(tabel_otr_vremeni.Year, '-',tabel_otr_vremeni.Month, '-', tabel_otr_vremeni.Day),'%Y-%m-%d') BETWEEN STR_TO_DATE(@Value1,'%Y-%m-%d') AND STR_TO_DATE(@Value2,'%Y-%m-%d')
AND tabel_otr_vremeni.ID_Sotrudnika = @ID
GROUP BY tabel_otr_vremeni.Month
ORDER BY 'Месяц';";

        //Запросы Print



        //Запросы авторизации

        public string Select_Avtorization = $@"SELECT CASE EXISTS(SELECT * FROM login WHERE login.Login = @Value1 AND login.Password = @Value2)
WHEN 1 THEN
(SELECT CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) 
FROM sotrudniki 
INNER JOIN login ON sotrudniki.ID_Sotrudnika = login.ID_Sotrudnika
WHERE sotrudniki.ID_Sotrudnika = (SELECT login.ID_Sotrudnika FROM login WHERE login.Login = @Value1 AND login.Password = @Value2)) END;";

        public string Exists_User = $@"SELECT EXISTS(SELECT * FROM login WHERE login.Login = @Value1 AND login.Password = @Value2);";

        public string Select_FIO_Usera = $@"SELECT CONCAT(sotrudniki.Familiya, ' ', sotrudniki.Imya, ' ', sotrudniki.Otchestvo) 
FROM sotrudniki 
INNER JOIN login ON sotrudniki.ID_Sotrudnika = login.ID_Sotrudnika
WHERE sotrudniki.ID_Sotrudnika = (SELECT login.ID_Sotrudnika FROM login WHERE login.Login = @Value1 AND login.Password = @Value2);";

        public string Select_Role = $@"SELECT CONCAT(roles.Doljnosti,roles.Grafik_Raboty,roles.Oklad,roles.Otdely,
roles.Raschetnye_Scheta,roles.Sotrudniki,roles.Tabel_Otr_Vremeni,roles.Vyplaty)
FROM roles INNER JOIN login ON roles.ID_Role = login.ID_Role
WHERE login.Login = @Value1 AND login.Password = @Value2;";


        //Запросы авторизации
    }
}

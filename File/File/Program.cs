public static void CreateObject()
{
    //Создание объекта на карте
}

public static void GetChance()
{
    _chance = Random.Range(0, 100);
}

public static int СalculateSalary(int hoursWorked)
{
    return _hourlyRate * hoursWorked;
}

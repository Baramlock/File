public static void GenerateNewObject()
{
    //Создание объекта на карте
}

public static void SetChance()
{
    _chance = Random.Range(0, 100);
}

public static int СalculateSalary(int hoursWorked)
{
    return _hourlyRate * hoursWorked;
}

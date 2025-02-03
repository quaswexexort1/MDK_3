
interface IInteger // Интерфейс для арифметических операций над числами.
{
    IInteger Add(IInteger other);
    IInteger Subtract(IInteger other);
    IInteger Multiply(IInteger other);
    void Print();
}

abstract class Integer : IInteger // Класс для представления целых чисел.

{
    protected int[] digits; // Массив для хранения цифр числа

    // Конструктор для создания объекта Integer из строки.
    public Integer(string value) => digits = value.Select(c => int.Parse(c.ToString())).ToArray(); // Преобразуем строку в массив цифр

    // Конструктор для создания объекта Integer из массива цифр.
    public Integer(int[] digits) => this.digits = digits; // Сохраняем массив цифр


    public abstract IInteger Add(IInteger other);
    public abstract IInteger Subtract(IInteger other);
    public abstract IInteger Multiply(IInteger other);
    public virtual void Print() => Console.WriteLine(string.Join("", digits.Reverse()));

    protected int[] GetDigits() => digits; // Метод для получения массива цифр

}

// Класс для представления десятичного числа.
class Decimal : Integer
{
    public Decimal(string v) : base(v) { } // Конструктор для создания десятичного числа из строки
    public Decimal(int[] d) : base(d) { } // Конструктор для создания десятичного числа из массива цифр


    // Реализация метода сложения для десятичных чисел
    public override IInteger Add(IInteger other)
    {
        var a = GetDigits();                  // Получаем массив цифр текущего числа
        var b = ((Decimal)other).GetDigits(); // Получаем массив цифр второго числа
        int carry = 0,                        // Перенос
            m = Math.Max(a.Length, b.Length); // Находим максимальную длину массивов
        int[] result = new int[m + 1];        // Создаем массив для результата с учетом возможного переноса

        for (int i = 0; i < m; i++)          // Проходим по цифрам с конца
        {
            int sum = carry;                               // Инициализируем сумму переносом
            if (i < a.Length) sum += a[a.Length - 1 - i]; // Добавляем цифру первого числа (с конца)
            if (i < b.Length) sum += b[b.Length - 1 - i]; // Добавляем цифру второго числа (с конца)
            result[m - i] = sum % 10;                     // Записываем остаток от деления на 10 в результат
            carry = sum / 10;                            // Обновляем перенос
        }
        if (carry > 0) result[0] = carry; // Если есть перенос, записываем его в начало
        return new Decimal(result.SkipWhile(x => x == 0).ToArray()); // Создаем новый объект и удаляем ведущие нули

    }

    // Реализация метода вычитания для десятичных чисел
    public override IInteger Subtract(IInteger other)
    {
        var a = GetDigits();
        var b = ((Decimal)other).GetDigits();
        int borrow = 0;                             // Заём
        int m = Math.Max(a.Length, b.Length);
        int[] result = new int[m];                 // Создаем массив для результата

        for (int i = 0; i < m; i++)                // Проходим по цифрам с конца
        {
            int diff = borrow;                                    // Инициализируем разность займом
            if (i < a.Length) diff += a[a.Length - 1 - i];       // Добавляем цифру первого числа (с конца)
            if (i < b.Length) diff -= b[b.Length - 1 - i];       // Вычитаем цифру второго числа (с конца)

            if (diff < 0)
            {
                diff += 10; // Если результат отрицательный, добавляем 10 и делаем заем
                borrow = 1;
            }
            else
                borrow = 0; // Обновляем заем

            result[m - 1 - i] = diff; // Записываем разность в результат

        }
        return new Decimal(result.SkipWhile(x => x == 0).ToArray()); // Создаем новый объект и удаляем ведущие нули

    }


    // Реализация метода умножения для десятичных чисел
    public override IInteger Multiply(IInteger other)
    {
        var a = GetDigits();
        var b = ((Decimal)other).GetDigits();
        int m = a.Length; // Длина массива первого числа
        int n = b.Length; // Длина массива второго числа
        int[] result = new int[m + n]; // Создаем массив для результата
           
        // Проходим по цифрам первого числа с конца
        for (int i = m - 1; i >= 0; i--)
        {
            int carry = 0;
            for (int j = n - 1; j >= 0; j--)
            {
                int product = a[i] * b[j] + result[i + j + 1] + carry; // Вычисляем произведение, сумму и перенос
                result[i + j + 1] = product % 10;                      // Записываем остаток от деления на 10
                carry = product / 10;
            }
            result[i] += carry;                                        // Добавляем перенос к текущему разряду
        }
        return new Decimal(result.SkipWhile(x => x == 0).ToArray());  // Создаем новый объект и удаляем ведущие нули

    }
}

// Класс для представления двоичного числа.
class Binary : Integer
{
    public Binary(string v) : base(v) { }
    public Binary(int[] digits) : base(digits) { }


    // Реализация метода сложения для двоичных чисел
    public override IInteger Add(IInteger other)
        {
        var a = GetDigits();
        var b = ((Binary)other).GetDigits();
        int carry = 0;
        int m = Math.Max(a.Length, b.Length); // Находим максимальную длину массивов
        int[] result = new int[m + 1];        // Создаем массив для результата

        for (int i = 0; i < m; i++)
        {
            int sum = carry; // Инициализируем сумму переносом
            if (i < a.Length) sum += a[a.Length - 1 - i];
            if (i < b.Length) sum += b[b.Length - 1 - i];

            result[m - i] = sum % 2; // Записываем остаток от деления на 2 в результат
            carry = sum / 2; // Обновляем перенос
        }
        if (carry > 0) result[0] = carry; // Если есть перенос, записываем его в начало
        return new Binary(result.SkipWhile(x => x == 0).ToArray()); // Создаем новый объект и удаляем ведущие нули
    }

    // Реализация метода вычитания для двоичных чисел
    public override IInteger Subtract(IInteger other)
    {
        var a = GetDigits();
        var b = ((Binary)other).GetDigits();
        int borrow = 0;
        int m = Math.Max(a.Length, b.Length); // Находим максимальную длину массивов
        int[] result = new int[m]; // Создаем массив для результата

        for (int i = 0; i < m; i++)
            {
            int diff = borrow; // Инициализируем разность займом
            if (i < a.Length) diff += a[a.Length - 1 - i];
            if (i < b.Length) diff -= b[b.Length - 1 - i];

            if (diff < 0)
            {
                diff += 2;
                borrow = 1;
            }
            else
                borrow = 0; // Обновляем заем

            result[m - 1 - i] = diff; // Записываем разность в результат
        }
        return new Binary(result.SkipWhile(x => x == 0).ToArray()); // Создаем новый объект и удаляем ведущие нули
    }


    // Реализация метода умножения для двоичных чисел
    public override IInteger Multiply(IInteger other)
    {
        var a = GetDigits();                      // Получаем массив цифр текущего числа
        var b = ((Binary)other).GetDigits();     // Получаем массив цифр второго числа
        int m = a.Length;                       // Длина массива первого числа
        int n = b.Length;                      // Длина массива второго числа
        int[] result = new int[m + n];        // Создаем массив для результата

        for (int i = m - 1; i >= 0; i--)
        {
            if (a[i] == 0) // Если текущая цифра первого числа равна нулю, переходим к следующей
                continue;

            int carry = 0;
             for (int j = n - 1; j >= 0; j--)
             {
                int product = b[j] + result[i + j + 1] + carry; // Вычисляем произведение, сумму и перенос
                result[i + j + 1] = product % 2; // Записываем остаток от деления на 2
                carry = product / 2; // Обновляем перенос
            }
            result[i] += carry; // Добавляем перенос к текущему разряду
        }
        return new Binary(result.SkipWhile(x => x == 0).ToArray());
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Создаем объекты Decimal
        IInteger d1 = new Decimal("123");
        IInteger d2 = new Decimal("456");

        // Вывод операций для Decimal
        Console.WriteLine("Decimal:");
        Console.Write($"{d1} + {d2} = "); d1.Add(d2).Print();
        Console.Write($"{d2} - {d1} = "); d2.Subtract(d1).Print();
        Console.Write($"{d1} * {d2} = "); d1.Multiply(d2).Print();
        Console.WriteLine();

        // Создаем объекты Binary
        IInteger b1 = new Binary("101");
        IInteger b2 = new Binary("110");

        // Вывод операций для Binary
        Console.WriteLine("Binary:");
        Console.Write($"{b1} + {b2} = "); b1.Add(b2).Print();
        Console.Write($"{b2} - {b1} = "); b2.Subtract(b1).Print();
        Console.Write($"{b1} * {b2} = "); b1.Multiply(b2).Print();
        Console.ReadKey();
    }
}


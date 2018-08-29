using System;

/*
  ЗАДАНИЯ:
1.	Реализовать структуру Point для хранения координат X и Y с функцией вывода этих значений на консоль.
2.	Реализовать стек с операциями записи значения в стек и извлечения значения из стека.
3.	Создать класс в интересной вам предметной области. Класс должен иметь несколько полей с разными типами, в том числе nullable types и enums. Определить конструктор по-умоланию и несколько свойств. Определить индексатор.
4.	Что нужно сделать, чтобы с классом можно было работать с помощью цикла foreach. - определить индексатор
5.	Реализовать структуру (иерархию) классов (от 3 штук) в интересной вам предметной области, используя наследование, полиморфизм. Задействуйте интерфейсы, абстрактные классы, абстрактные и виртуальные функции. 
    Напишите программу для демонстрации вызова виртуальных функций у объектов различных классов из вашей иерархии.
*/

namespace Csharp_LabRab5{
    struct Point
    {
        public int x_coord, y_coord;           //координаты точек x и y
        
        public void info() {
            Console.WriteLine($"x = {x_coord}, y = {y_coord}.");    
            //обращаем внимание на то, что при использовании в индексируемых местозаполнителях полей структуры, перед фиксированным текстом необходимо поставить символ "$".
        }
    }                         //структура для задания 1
    
    public class MyStack<T> {               //класс стека. <T> указывает на то, что клсс является обобщенным, и T далее будет использоваться как тип данных при работе с данными
        private T[] elements;               //объявление массива типа T, сюда будут складываться элементы стека
        private int count;                  //указатель на элемент массива

        public MyStack() {                  //конструкор класса
            elements = new T[1];            //инициаллизация массива элементов
            count = 0;                      //инициализация счетчика
        }

        public void push(T element) {       //метод помещения нового элемента в массив
            elements[count] = element;      //помещаем новый элемент в массив с текущим указателем на индекс 
            Console.WriteLine("В стек помещен элемент {0}, его № {1}.", elements[count], count);
            count++;                        //инкрементируем указатель на элемент массива
            Array.Resize(ref elements, elements.Length + 1);    //т.к. Array не является динамическим, то используем метод Resize для задания новой границы массива
        }
        
        public string pull() {              //метод извлечения нового элемента в массив, будем конвертировать в строку
            string result;
            count--;                        //декрементируем указатель на элемент массива
            result = ("Извлекается элемент стека "+Convert.ToString(elements[count])+", его № "+(count)+".");
            Array.Resize(ref elements, elements.Length - 1);    //уменьшаем размер массива
            return result;
        }

        public void showMyStack() {         //метод вывода элементов стека в консоль
            for (int i = 0; i < count; i++)
                Console.WriteLine("Элемент стека №{0} - {1}", i, elements[i]);
        }
    }             //класс для задания 2

    public enum Country {                   //перечисление стран
        Russia = 1,
        USA,
        EU
    };

    interface FinInfo {
       void printInfo();
    }

    public abstract class FinInstrument : FinInfo  {   //абстрактный класс для задания 3. мое дополнение - имплементация интерфейса FinInfo и реализация его метода
        //СТРУКТУРА ДАННЫХ
        public enum Currency { RUB = 1, USD, EUR };    //перечисление валют, в которых торгуется финансовый инструмент
        decimal? price, lowest_price, highest_price; //"?" после типа значения указывает на то, что поле содержит значение null. Эквивалентно System.Nullable<T>, где T - тип значения
        string issuer;      //эмитент           //напр. Alphabet inc.
        public Currency nati_curr; //в какой нацвалюте торгуется инструмент
        Country country;    //страна-эмитент

        public string Ticker { get => ticker; set => ticker = value; } //следим за пальцами: это имя свойства, реализующего методы доступа (аксессоры) get и set/ 
        string ticker;//тикер             //напр. GOOG                 //а это - инкапсулируемое свойством поле

        public string Name { get => name; set => name = value; }
        string name;        //имя инструмента   //напр. Alphabet inc. class C

        string issuer_country { get; set; }     //страна эмитента; автоматическое реализуемое свойство с методами доступа get и set (ПОЛЕ СОЗДАНО ДЛЯ ПРИМЕРА)
        public string Issuer                    //еще одна реализация свойства c методами доступа get и set
        {
            get => issuer;                      //получаем имя эмитента по обращению *имя объекта*.Issuer
            set => issuer = value;              //пишем значение так же: *имя объекта*.Issuer = *значение*
        }
        
        //КОНСТРУКТОРЫ
        public FinInstrument() {    //переопределение конструктора по умолчанию - будут устанавливаться значения перечислений по умолчанию
            nati_curr = Currency.USD;
            country = Country.USA;
        }

        public FinInstrument(Country country, string issuer, string ticker) {   //параметрический конструктор

            switch (country) {      //в зависимости от инкапсулируемого значение enum Country задается значение enum Currency, входящее в класс FinInstrument
                case Country.Russia:
                    nati_curr = Currency.RUB;
                    break;
                case Country.USA:
                    nati_curr = Currency.USD;
                    break;
                case Country.EU:
                    nati_curr = Currency.EUR;
                    break;
            }
            this.issuer = issuer;       //через this инкапсулируется значение поля именно данного экземпляра объекта, который создается настоящим конструктором
            this.ticker = ticker;       //такой способ можно использовать для самописных методов доступа get и set. здесь же стоит упомянуть, что через super. можно получить доступ к значению поля не объекта, а суперкласса
        }

        //здесь я бы расположил индексатор, если бы имелась какая-либо внятная и адекватная сущность, которую стоило бы реализовать с помощью массива.

        //МЕТОДЫ
        public abstract void printInfo();   //в абстрактном классе метод имплементируемого интерфейса реализуется абстрактно - т.е. о самом методе упоминание есть, но конкретной реализации нет.

        public abstract void showInstrumentInfo(); //абстрактная функция, не имплементированная через интерфейс. требуется последующая реализация в классах-потомках
       
        public virtual void showTicker(){           //виртуальная функция. можно использовать в классах-наследниках "как есть", либо переопределить при написании наследника
            Console.WriteLine(Name+"'s ticker is "+Ticker);
        }
    }

    class Stock : FinInstrument {           //класс акция - наследник класса финансовый инструмент
        public Stock(string issuer, string ticker) {//конструктор класса Stock
            Ticker = ticker;
            Issuer = issuer;
        }
        public Stock(string name, string issuer, string ticker)
        {//конструктор класса Stock
            Name = name;
            Ticker = ticker;
            Issuer = issuer;
        }

        override public void printInfo() {          //реализация метода printInfoprintInfo имплементированного классом FinInstrument интерфейса FinInfo
            Console.WriteLine("Информация о ценной бумаге компании {0}: эмитент {1}, тикер {2}, торгуется в {3}\n", Name, Issuer, Ticker, this.nati_curr);
        }

        override public void showInstrumentInfo() { //обязательное переопределение реализации абстрактного метода (функции)
            Console.WriteLine("Акция - это ценная бумага, позволяющая наращивать капитализацию компании, а инвестору - получать ежеквартальный дивидендный доход и рассчитывать на доход от последующей ее продажи.\n");
        }

        /*override public void showTicker(){          //не обязательное переопределение виртуальной функции 
            Console.WriteLine("Тикер акции - "+Ticker+". По тикеру легко можно найти акцию на информационных порталах.\n");
        }*/

    }                 

    class Currency : FinInstrument          {//класс валюта - наследник класса финансовый инструмент
        override public void printInfo()
        {  //реализация метода printInfoprintInfo имплементированного классом FinInstrument интерфейса FinInfo
            Console.WriteLine("Информация о валюте {0}: эмитент {1}, тикер {2}.\n", Name, Issuer, Ticker);
        }
        public override void showInstrumentInfo()
        {
            Console.WriteLine("Это валюта - универсальное инструмент обмена.\n");
            throw new NotImplementedException();
        }
    }              

    class CryptoCurrency : Currency { }             //класс криптовалюта - наследник класса валюта

    class ETF : FinInstrument {                     //класс ETF - наследник класса финансовый инструмент, содержащий массив - портфель разных акций.

        Stock[] stockPortfolio;

        //КОНСТРУКТОР
        public ETF(string name, int howStocks) {
            stockPortfolio = new Stock[howStocks];  //задание границ массива Stock
            Name = name;
            Console.WriteLine("ETF "+name+" создан, помещено {0} акций в портфель.\n", howStocks);
        }
        
        //ИНДЕКСАТОР ПО НОМЕРУ
        public Stock this[int index] {

            get {
                return stockPortfolio[index];
            }
            set {
                stockPortfolio[index] = value;
            }
        }

        //ИНДЕКСАТОР ПО ТИКЕРУ АКЦИИ - ФАКТИЧЕСКИ ПЕРЕГРУЖЕННЫЙ ИНДЕКСАТОР
        public Stock this[string isticker]
        {
            get
            {
                Stock stock = null;
                foreach (var p in stockPortfolio)   //вот и реализация foreach - для каждого следующего p в массиве stockPortfolio. по в данном перегруженном аксессоре происходит поиск 
                                                    //по элементам массива методом перебора каждого и сравнения значений параметра с полем элемента массива
                {
                    if (p?.Ticker == isticker)      //, где p? - p-й элемент массива акций Stock stockPortfolio. ВОПРОС - а как ticker оставить сокрытым? ОТВЕТ - а с помощью быстрых действий инкапсулируем ticker и создаем свойство Ticker
                    {                               //кстати, далее по тексту ticker везде заменится на свойство Ticker. неплохо, MSFT, но нахрен не нужно - генерирование геттеров и сеттеров в Java намного проще и удобнее
                        stock = p;                  //ссылочному типу stock присвоить значение очередного элемента p
                        break;
                    }
                }
                return stock;                       //и вернуть как результат Stock stock
            }
        }
        //МЕТОД
        override public void printInfo() {          //реализация метода printInfoprintInfo имплементированного классом FinInstrument интерфейса FinInfo
            Console.WriteLine("Всего в портфеле ETF фонда {0} находится {1} акций.\n", Name, stockPortfolio.Length);
        }

        public override void showInstrumentInfo() {
            Console.WriteLine("Это ETF-фонд - дериватив, который содержит ценные бумаги одного сектора экономики. Покупая его, вы покупаете готовый портфель бумаг под управлением профессионалов.\n");
        }
        public override void showTicker(){
            Console.WriteLine("Переопределенный метод showTicker().\n");
        }
        public void showTicker(int i){              //виртуальная функция. можно использовать в классах-наследниках "как есть", либо сделать перегрузку
            Console.WriteLine("А это {0}-я реализация виртуального метода showTicker()", i);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("***ЗАДАНИЕ 1***\n");
                Point p = new Point();                          //создаем новую структуру
                p.x_coord = 2;                                  //указывае значение координаты х
                p.y_coord = 3;                                  //указывае значение координаты y
                p.info();                                       //выводим информацию о координатах

                Console.WriteLine("\n***ЗАДАНИЕ 2***\n");
                //первая реализация обобщенного класса стека - целочисленная
                MyStack<int> Mstack = new MyStack<int>();       //создаем новый стек, задаем тип <int>
                Mstack.push(p.x_coord);                         //занесем в него координаты из реализованной структуры
                Mstack.push(p.y_coord);
                Mstack.showMyStack();                           //выведем элементы стека на экран
                Console.WriteLine(Mstack.pull());               //и достанем элементы из стека
                Console.WriteLine(Mstack.pull());               //и еще раз

                //еще одна реализация обобщенного класса стека - булевая
                bool b1 = true;                                 //для чистоты эксперимента объявим и инициализируем булевую переменную b1 с значением true
                MyStack<bool> boolStack= new MyStack<bool>();   //создаем новый стек, задаем тип <bool>
                boolStack.push(b1);                             //заносим в него первую переменную b1
                boolStack.push((p.x_coord>p.y_coord));          //и результат проверки условия 
                boolStack.showMyStack();                        //распечатаем и посмотрим, что будет хранить стек

                Console.WriteLine("\n***ЗАДАНИЕ 5***\n");
                ETF highTech = new ETF("AlexCapital", 5);
                highTech.printInfo();
                highTech.showInstrumentInfo();
                
                highTech[0] = new Stock("Microsoft inc.", "MSFT");  //результат работы индексатора - можно обращаться к созданному классу hightek[i], где i - индекс (номер) акции (обхекта Stock) в портфеле фонда ETF (массиве класса ETF)
                highTech[1] = new Stock("nVidia", "nVidia inc.", "NVDA");     //в данных случая по индексной ссылке создается новый объект класса Stock со своими параметрами
                highTech[0].Name = "Microsoft";                     //запись значенея в инкапсулируемую переменную через сеттер

                Console.WriteLine("Поговорим о виртуальном методе.\nВ абстрактном классе существует виртуальный метод public virtual void showTicker(). \nВ классах-наследниках его можно переопределить через override:\n");
                highTech.showTicker();      //выхов переопределенной классом ETF виртуальной функции
                highTech.showTicker(2);     //вызов перегруженной переопределенной классом ETF виртуальной функции
                Console.WriteLine("Cтандартная реализация метода public virtual void showTicker() может быть вызвана наследуемым объектом, класс которого не переопределял данный метод, как Stock:");
                highTech[1].showTicker();   //вызов объектом класса Stock виртуальной функции абстрактного класса

                Console.WriteLine("\nТеперь будем играть с индексаторами класса Stock:\nТикер акций {0} - {1}, эмитент {2}\n", highTech[0].Name, highTech[0].Ticker, highTech[0].Issuer); //результат работы целочисленного индексатора - можно обращаться к акции в портфеле ETF по номеру
                highTech[0].printInfo();    //этот метод имплементирован абстрактным классом FinInstrument, от которого унаследован Stock, в котором метод интерфейса переопределен - так и вызывается
                Console.WriteLine("Один из лидеров роста NASDAQ-100 - "+highTech["NVDA"].Issuer+"\n");         //результат работы строкового индексатора - можно обращаться к акции в портфеле ETF по тикеру
                highTech["NVDA"].printInfo();               //через строковый индексатор также можно обращаться к объекту класса Stock через объект класса ETF и вызывать нужный метод.
                highTech["NVDA"].showInstrumentInfo();      //вызов переопределенного метода, изначально объявленного как абстрактный в абстрактном классе-предке FinInstrument
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally {
                Console.WriteLine("\n<Press any button...>");
                Console.ReadKey();
            }
        }
    }
}
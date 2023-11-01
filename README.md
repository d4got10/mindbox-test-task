<h1 align="center">Area calculator</h1>

# Задача 1

## Общая концепция библиотеки

Для работы с библиотекой нужно создать объект типа AreaCalculator

Он имеет два публичных метода: <br>```ErrorOr<double> CalculateArea(IShape shape)``` <br>и <br>```ErrorOr<double> CalculateArea<T>(T shape) where T : IShape``` 



Пользователь должен создать нужную ему фигуру с данными и передать её в метод. На выходе он получает либо ошибку, если вычисления не возможны, либо результат вычисления.



Работа происходит в 3 этапа:

* Проверка поддержки типа фигуры

* Валидация данных фигуры

* Расчёт площади фигуры

## Пример

Пример использования библиотеки можно посмотреть в проекте AreaCalculation.Example

## Возможные ошибки

Тип ошибки записывается в свойство **Code**

* **NullArgument** - аргументом был передан **null**

* **UnsupportedType** - тип переданной фигуры не поддерживается библиотекой

* **InvalidShape** - данные фигуры не являются корректными (например, радиус круга отрицателен)

Так же в ошибке присутствует подробное описание в свойстве **Description**

## Замечание

Так как эта библиотека будет предоставляться клиенту исключительно для расчётов площадей, я предположу, что в предметной области клиента уже есть сущности для различных форм. Для использования библиотеки будет происходить сопоставление их сущностей с сущностями библиотеки. 
Если использовать **ссылочный тип** для представления данных фигур, то после преобразования будет образовываться мусор и снижаться **производительность**. Поэтому для данных фигур целесообразней использовать **структуры**. 
Но передача **структуры** по **интерфейсу** влечет за собой **boxing/unboxing**. Для этого и был добавлен **обобщенный метод**.

## Поддерживаемые фигуры

* Треугольник (задается длинами трёх сторон)

* Круг (задается радиусом)

## Добавление других фигур

#### На стороне библиотеки

Поддержка фигуры достигается с помощью трёх сущностей:

* Модель данных, наследуется от ```IShape``` (Например, ```Triangle```)

* Валидатор модели данных, наследуется от ```IShapeValidator<>``` (Например, ```TriangleValidator : IShapeValidator<Triangle>```)

* Стратегия вычисления площади, наследуется от ```IAreaCalculationStrategy<>``` (Например, ```TriangleAreaCalculationStrategy : IAreaCalculationStrategy<Triangle>```)

И добавлением их в ```AreaCalculator.ShapeSupports```

#### На стороне клиента

Если нет доступа к ```AreaCalculator.ShapeSupports```, то есть возможность наследования от ```AreaCalculator``` и добавления поддержки из класса наследника. Для этих целей ```ShapeSupports``` сделан ```protected```.

## Тестирование

Unit-тесты расположены в проекте AreaCalculation.Tests

Использовался NUnit

## Дополнительно

Была использована библиотека [ErrorOr](https://github.com/amantinband/error-or) для возврата ошибок как значений вместо бросания исключений конструкцией throw.

# Задача 2

**Схема данных** описана в файле **mssql-schema**<br>**Запрос** находится в файле **mssql-query**

Есть две основные таблицы - **Product** и **Category** и связующая таблица **ProductCategory**



#### Возможные оптимизации

* Индексация ProductCategory.ProductId и ProductCategory.CategoryId для ускорения Join-ов

* Пагинация



# Лексический анализатор

[Код Синтаксического анализатора](codeWindowsFormsApp1CoreParser)

## Грамматика

```
a constant numeric = 5 ;
1.	ID - letter  IdRem
2.	IdRem - (  letter   number ) idRem
3.	idRem - const
4.	const - ‘constant’  type
5.	type -  (‘numeric’  … ) assign
6.	assign -  ‘’ assignRem
7.	assignRem - ‘=’ number
8.	number - [+-] numberRem
9.	numberRem - digit numberRem
10.	numberRem - end
11.	numberRem - '.’ numberRemRem
12.	numberRemRem - digit numberRemRem
13.	numberRemRem -  end
14.	end - ;


```

## Граф конечного автомата 
![Диаграмма сканера](stateMachineGraph.jpg)

## Тестовый запрос
`a constant numeric = 5 ;`
![Пример работы](success.png)



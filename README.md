# Руководство для начинающих пользователей в PaintProject <!--- слово "новичок" может для некоторых звучат обидно, я бы предложила написать более нейтрально --->

**Добро пожаловать в подробное руководство для начинающих пользователей по PaintProject!** 

**О чем пойдет речь в этой статье?**

- [Что такое PaintProject](#Что-такое-PaintProject)
- [Установка](#Установка)
  - [Windows](#Windows)
- [Как пользоваться PaintProject](#Как-пользоваться-PaintProject)
  - [Инструменты рисования](#Инструменты-рисования)
  - [Толщина](#Толщина)
  - [Выбор цвета](#Выбор-цвета)
  - [Холст](#Холст)
  - [Файл](#Файл)
  - [Расположение окон](#Расположение-окон)
  - [Расширения](#Расширения)
- [Дополнительные советы](#Дополнительные-советы)
- [Устранение неполадок и поддержка](#Устранение-неполадок-и-поддержка)
- [Работа с исходным кодом](#Работа-с-исходным-кодом)

------

# Что такое PaintProject

**Учебный проект** для получения опыта работы с плагинами, MDI и технологией рефлексии. В версии 0.4 доступны следующие возможности:
* рисование обычной кистью;
* построение простых геометрических фигур;
* возможность сохранения холста в следующих форматах: .BMP /. JPG / .PNG. <!--- можно оформить списком --->

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/01.png)

------

# Установка

## Windows 

1. Скачать [установочный файл](https://github.com/makspepe/Paint/releases/download/0.4/Paint.Setup.msi) (В настоящий момент поддержка Windows 7 и более поздних).

2. Следовать инструкциям мастера установки.

    ![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/1install.PNG)

Ярлык приложения появится на рабочем столе, и вы можете начать работу с проеком. ![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/2install.png)

------

# Как пользоваться PaintProject

## Инструменты рисования

Чтобы выбрать инструмент для рисования: 
1. В меню в выпадающем списке выберите нужный инструмент и нажмите на него.

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/3.0ris.png) <!--- стоит более аккуратно оформять (относится ко всем) --->

### Виды инструментов:

* **Обычная кисть** - имитирует ручную кисть. Он используется для создания полос сплошного цвета. Чтобы рисовать с помощью этого инструмента, зажмите левую кнопку мыши и ведите кисть по полотну. 

  > Форма кончика кисти всегда круглая. 

* **Линия** - используется для рисования линий. Чтобы нарисовать линию, укажите точку начала линии с помощью левой кнопки мыши и проведите линию к желаемой конечной точке. Освобождение кнопки мыши завершает этап рисования.

* **Круг** - используется для создания эллипса. Чтобы нарисовать эллипс,  укажите точку, располагающуюся на линии фигуры и растяните фигуру до желаемого размера.

  > Совет! Эллипс будет иметь равные ширину и высоту при вытягивании, если удерживать клавишу **Shift**. ⭕️

* **Звезда** - создает звезду ⚝ фиксированного размера вокруг указателя мыши.

* **Ластик** - отмечает области холста как прозрачные и стирает нарисованные фигуры.

  

## Толщина

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/3.1ris.PNG)

Это толщина (в пикселях) кончика кисти/линии. 



## Выбор цвета

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/3.2ris.png)

Цвет, применяемый к выбранному инструменту рисования. Доступные следующие цвета:

* Красный 

* Синий

* Зеленый

Для задания другого цвета нажмите кнопку *Другой* и задайте пользовательский цвет в одной из систем:
* шестнадцатеричная (HEC/[RGB-формат[(https://ru.wikipedia.org/wiki/RGB);
* десятичная (DEC). <!--- тоже добавить ссылку --->

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/3.3ris.PNG)



## Холст

Чтобы изменить размер холста, перейдите на вкладку **Рисунок** и нажмите **Размер холста**. В открывшемся окне вы можете увеличить или уменьшить размер (в пикселях) выделенного холста.

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/3.4plug.png)



## Файл

На вкладке **Файл** вы можете:

- Открыть новый холст.
- Открыть существующее изображение в следующих форматах: .BMP / .JPG / .PNG. <!--- можно списком оформить --->
- Сохранить изменения.
- Сохранить выбранный холст в другом формате.
- Выйти из приложения, если ваш курсор оказался с левой стороны окна, кнопки ALT+F4 не работают, а каждая доля секунды на счету. <!--- Не очень понятны пояснения. Я бы оставить просто действие "выйти из приложения"  --->

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/3.5.png)



## Расположение окон

На вкладке **Окна** вы можете выбрать формат раасположени окон приложения:

* Упорядочить значки (сеткой);
* Слева на право; 
* Сверху вниз;
* Каскадом (окна поверх друг друга).

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/3.6.png)



## Расширения

Все загружаемые расширения отображаются при запуске:

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/4.0plug.png)

Для работы с расширениями перейдите на соответствующую вкладку:

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/4.1plug.png)

На данный момент доступны следующие расширения:

* Переворот изображения (переворачивает изображение на 90 градусов);
* Медианный фильтр (цвет каждого пикселя заменяется на среднее значение соседних пикселей).

------

# Дополнительные рекомендации

- Так как в [PaintProject](#Что-такое-PaintProject) нет слоёв, используйте сочетание клавиш **ctrl+z** для отмены последнего действия.
- Не забывайте о возможности вставки изображений из буфера.
- Изменяйте масштаб рисунка при помощи колеса мыши (прокрутка на себя - уменьшение, прокрутка от себя - увеличение).

------

# Устранение неполадок и поддержка

Если у вас возникнут вопросы или предложения, передайте их в сообщении [службе поддержки приложения](https://github.com/makspepe/Paint/issues).

------

# Работа с исходным кодом

Вы можете редактировать и компилировать проект как на Windows, так и на Linux и Mac OS.

1. Нажмите **Clone > Download ZIP**, чтобы скачать [архив с кодом](https://github.com/makspepe/Paint/archive/master.zip):

![](https://raw.githubusercontent.com/makspepe/Paint/master/pics_tutorial/99source.png)

2. Распакуйте архив.

> Вы можеете использовать следующую команду:
>
> ```sh
> 	git clone https://github.com/makspepe/Paint.git
> ```
>

3. Запустите файл *Paint.sln* в Visual Studio Code или Visual Studio (версии 2017 и позднее).

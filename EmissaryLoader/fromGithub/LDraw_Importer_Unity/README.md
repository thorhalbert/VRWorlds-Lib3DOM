# LDraw_Importer_Unity

# From https://github.com/Nox7atra/LDraw_Importer_Unity - Grygory Dyadichenko - MIT License - Fork on 08/31/2020

Profound thanks to Grygory for this code...  This branch looks to being kept current - please go star/fork Grygory's code.

I am not doing a conventional fork because I have to rather drastically change how it works.   I am also going to make into a true class library.

Looking to use this an initial simple model for VR objects.   We'll allow imports from lego cad drawings, stl, and any other mesh importer we can find, and eventually stored our own mesh format.   


# Original Readme:

Пример работы: https://habr.com/post/433364/

Чтобы открыть плагин выберите - **Window -> LDrawImporter -> Open Importer**

Плагин обладает двумя режимими: **ByName** и **Models**

**ByName** - генерирует любую деталь или модель по её имени (писать без расширения)

**Models** - выводит список моделей, которые лежат по пути LDrawFiles/blueprints/models/ (модели можно найти тут omr.ldraw.org)

По нажатию на кнопку **Generate** - сгенерируется выбранная модель в выбранной сцене.
Если **Generate** выдаёт ошибку и не генерирует модель - попробуйте нажать **Update bluepritns** или следуйте указаниям из ошибки.

**Update bluepritns** - обновляет доступные чертежи, если вы закинули новые в папку с ldraw файлами

Copyright 2018 Grygory Dyadichenko

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

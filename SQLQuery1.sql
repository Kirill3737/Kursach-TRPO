update CarMark 
set photo = (SELECT MyImage.* from Openrowset(Bulk 'E:\Политех\4 курс\ТРПО Зернова\Курсач ТРПО Зернова Копия Изображение\1.jpg', Single_Blob) MyImage) where id_mark = 7

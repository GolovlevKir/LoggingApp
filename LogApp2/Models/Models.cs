using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogApp2.Models
{
    public class Users
    {
        [Key]
        //Первичный ключ
        public int Id { get; set; }
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }
        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public int Age { get; set; }
    }

    public class mvcLoggingModel
    {
        [Key]
        //Первичный ключ
        public int Id { get; set; }
        [Display(Name = "Наименование устройства")]
        [Required(ErrorMessage ="Поле обязательно для заполнения")]
        public string MachineName { get; set; }
        [Display(Name = "Дата и время добавления")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public DateTime Logged { get; set; }
        [Display(Name = "Уровень логирования")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Level { get; set; }
        [Display(Name = "Сообщение")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Message { get; set; }
        [Display(Name = "Источник")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Logger { get; set; }
        [Display(Name = "Сайт источника")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Callsite { get; set; }

    }

    public class NLogs
    {
        [Key]
        //Первичный ключ
        public int Id { get; set; }
        public string MachineName { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }

    }

    public class UrlQuery
    {
        public string DataStart { get; set; }
        public bool IspDataStart { get; set; }
        public string DataEnd { get; set; }
        public bool IspDataEnd { get; set; }
        public string PoiskInText { get; set; }
        public string YrovenLog { get; set; }
        public bool HaveFilter => !string.IsNullOrEmpty(PoiskInText) || !string.IsNullOrEmpty(YrovenLog) || IspDataStart || IspDataEnd;

    }
}

﻿using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Business.Jobs
{
    public class EmailSenderSheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailSenderJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                .WithIntervalInMinutes(60)          // через  30 минут
                .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер
            scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }
    }
}

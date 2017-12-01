function Milliseconds(date) {
    var number_of_leap_years = Math.floor((date.getFullYear() - 2001) / 4) + 1;
    var number_of_normal_years = date.getFullYear() - 2000 - number_of_leap_years;
    var number_of_days = 0;
    for (var i = 0; i < date.getMonth() ; i++) {
        var month = i + 1;
        if (month == 2 && date.getFullYear() % 4 == 0)
            number_of_days += 29;
        else if (month == 2)
            number_of_days += 28;
        else if (month == 4 || month == 6 || month == 9 || month == 11)
            number_of_days += 30;
        else
            number_of_days += 31;
    }
    number_of_days += date.getDate();
    return number_of_leap_years * 366 * 86400000 + number_of_normal_years * 365 * 86400000 + number_of_days * 86400000 + date.getHours() * 3600000
        + date.getMinutes() * 60000 + date.getSeconds() * 1000 + date.getMilliseconds();
}
function DifferenceBetweeenDates(date_start, date_finish) {
    return Milliseconds(date_finish) - Milliseconds(date_start);
}
function MaskInt(InPut) {
    var IsNegative = InPut < 0;
    if (IsNegative) InPut = InPut * (-1);
    var result = InPut.toString();
    var CountZero = InPut.toString().length - 1;
    var CountSpaces = Math.floor(CountZero / 3);
    var index = 0;
    for (var i = 0; i < CountSpaces; i++) {
        index = (CountZero % 3) + 1 + i * 4;
        result = result.substring(0, index) + " " + result.substring(index);
    }
    if (IsNegative)
        return "-" + result;
    else
        return result;
}
function MaskDecimal(InPutD) {
    var IsNegative = InPutD < 0;
    if (IsNegative) InPutD = InPutD * (-1);
    var result = Math.floor(InPutD).toString();
    var CountZero = Math.floor(InPutD).toString().length - 1;
    var CountSpaces = Math.floor(CountZero / 3);
    var index = 0;
    var DrobChastyna = parseFloat((InPutD - Math.floor(InPutD)).toFixed(2));
    for (var i = 0; i < CountSpaces; i++) {
        index = (CountZero % 3) + 1 + i * 4;
        result = result.substring(0, index) + " " + result.substring(index);
    }
    if (DrobChastyna == 0)
        result += ",00";
    else if (DrobChastyna.toString().length == 3)
        result += DrobChastyna.toString().substring(1).replace('.', ',') + "0";
    else
        result += DrobChastyna.toString().substring(1).replace('.', ',');
    if (IsNegative)
        return "-" + result;
    else
        return result;
}
function FormatNumberForDate(number) {
    if (number < 10)
        return "0" + number;
    else
        return number;
}
function FormatDate(date, style) {
    if (style == 1)
        return FormatNumberForDate(date.getDate()) + "." + FormatNumberForDate(date.getMonth() + 1) + "." + FormatNumberForDate(date.getFullYear());
    else if (style == 2)
        return FormatNumberForDate(date.getDate()) + "." + FormatNumberForDate(date.getMonth() + 1) + "." + FormatNumberForDate(date.getFullYear()) + " "
            + FormatNumberForDate(date.getHours()) + ":" + FormatNumberForDate(date.getMinutes()) + ":" + FormatNumberForDate(date.getSeconds());
    else
        return FormatNumberForDate(date.getDate()) + "." + FormatNumberForDate(date.getMonth() + 1) + "." + FormatNumberForDate(date.getFullYear()) + " "
            + FormatNumberForDate(date.getHours()) + ":" + FormatNumberForDate(date.getMinutes()) + ":" + FormatNumberForDate(date.getSeconds()) + "."
            + FormatNumberForDate(date.getMilliseconds());

}
function AddDays(date, days) {
    return new Date(date.getFullYear(), date.getMonth(), date.getDate() + days, date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());
}
function AddMonths(date, month) {
    var addMonth = date.getMonth() + month;
    var NormaladdMonth = (addMonth + 1) % 12;
    if (NormaladdMonth == 2) {
        if (date.getDate() > 29 && new Date(date.getFullYear(), addMonth).getFullYear() % 4 == 0)
            return new Date(date.getFullYear(), addMonth, 29, date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());
        else if (date.getDate() > 28)
            return new Date(date.getFullYear(), addMonth, 28, date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());
        else
            return new Date(date.getFullYear(), addMonth, date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());
    }
    else if (NormaladdMonth == 4 || NormaladdMonth == 6 || NormaladdMonth == 9 || NormaladdMonth == 11)
        if (date.getDate() > 30)
            return new Date(date.getFullYear(), addMonth, 30, date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());
        else
            return new Date(date.getFullYear(), addMonth, date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());
    else
        return new Date(date.getFullYear(), addMonth, date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds());
}
function TotalDays(date_start, date_finish) {
    return Math.floor(DifferenceBetweeenDates(date_start, date_finish) / 86400000);
}
function IfIncreaseStrokKredytu(DataRozrahunku, DataPershogoPlatezhu) {
    return TotalDays(AddMonths(DataPershogoPlatezhu, -1), DataRozrahunku) > 2;
}
function IfBodyIsZero(DataPo4atkuProc, DataPershogoPlatezhu) {
    return TotalDays(DataPo4atkuProc, DataPershogoPlatezhu) <= 16;
}
function SumaProcWPeriodi(OstatokKredytu, Stawka, DataStart, DataEnd) {
    var k = Math.floor(TotalDays(DataStart, DataEnd));
    return (k * OstatokKredytu * Stawka) / 36000;
}
function AnnuitySchedule(SumaKredytu, StrokKredytu, ListStawok, Komisiya) {
    var DataRozrahunku = new Date();
    var DataPo4atkuProc = DataRozrahunku;
    var DataPershogoPlatezhu = AddDays(DataRozrahunku, TotalDays(DataRozrahunku, AddMonths(DataRozrahunku, 1)) - DataRozrahunku.getDate() + 1);
    var DataOplatyKredytu = AddDays(AddMonths(DataRozrahunku, StrokKredytu), -1);
    //var Platizh = [];
    var Schedule = new Array();
    var i = 0;
    var SumaKomisiyi = parseFloat((SumaKredytu * Komisiya / 100).toFixed(2));


    var StawkaMax = 0;
    for (var k = 0; k < ListStawok.length; k++) {
        StawkaMax = Math.max(StawkaMax, ListStawok[k][0]);
    }

    if (IfIncreaseStrokKredytu(DataRozrahunku, DataPershogoPlatezhu))
        StrokKredytu++;

    var MinSumaAnuiteta = SumaKredytu / StrokKredytu + SumaKomisiyi;
    var MaxSumaProcZaMisyac = SumaKredytu * 31 * StawkaMax / 36000;

    var MaxSumaAnuiteta = MinSumaAnuiteta + MaxSumaProcZaMisyac;

    var KinecObrahunku = false; var ChyTiloZero = false;
    var count = 1;
    while (true) {
        //Platizh.length = 0;
        Schedule.length = 0;
        ChyTiloZero = IfBodyIsZero(DataPo4atkuProc, DataPershogoPlatezhu);
        var DataStart = DataPo4atkuProc;
        var DataEnd = DataPershogoPlatezhu;

        var TiloFull = 0; var ProcentFull = 0; var WnesokFull = 0;
        OstatokKredytu = SumaKredytu;
        SumaAnuiteta = (MinSumaAnuiteta + MaxSumaAnuiteta) / 2;

        i = 0; var s = 0;

        var DataZminy = new Date(1900, 0);
        while (true) {
            Schedule.length += 1;
            Schedule[i] = new Array(6);
            DataZminy = AddMonths(DataRozrahunku, ListStawok[s][1]);
            var ChyZminaStawky = DataStart <= DataZminy && DataEnd > DataZminy && ListStawok.length > s + 1;
            //var Proc = 0;
            if (ChyZminaStawky) {
                Schedule[i][2] = SumaProcWPeriodi(OstatokKredytu, ListStawok[s][0], DataStart, DataZminy)
                    + SumaProcWPeriodi(OstatokKredytu, ListStawok[s + 1][0], DataZminy, DataEnd);
                s++;
            }
            else
                Schedule[i][2] = SumaProcWPeriodi(OstatokKredytu, ListStawok[s][0], DataStart, DataEnd);

            if (KinecObrahunku)
                Schedule[i][2] = parseFloat(Schedule[i][2].toFixed(2));


            if (SumaAnuiteta - SumaKomisiyi >= OstatokKredytu - (SumaAnuiteta - SumaKomisiyi) / 2 && (i + 1) == StrokKredytu) {
                Schedule[i][3] = OstatokKredytu;

            }
            else if (ChyTiloZero) {
                Schedule[i][3] = 0;

            }
            else {
                Schedule[i][3] = Math.min(OstatokKredytu, Math.max(SumaAnuiteta - Schedule[i][2] - SumaKomisiyi, 0));
            }


            Schedule[i][4] = OstatokKredytu - Schedule[i][3];
            Schedule[i][5] = DataEnd;
            Schedule[i][0] = FormatDate(DataEnd, 1);
            DataStart = DataEnd;
            if (AddMonths(DataPershogoPlatezhu, (i + 1)) > DataOplatyKredytu)
                DataEnd = DataOplatyKredytu;
            else
                DataEnd = AddMonths(DataPershogoPlatezhu, (i + 1));
            OstatokKredytu = OstatokKredytu - Schedule[i][3];
            if (OstatokKredytu == 0)
                Schedule[i][1] = Schedule[i][2] + Schedule[i][3] + SumaKomisiyi;
            else if (ChyTiloZero) {
                Schedule[i][1] = parseFloat(Schedule[i][2].toFixed(2));
                ChyTiloZero = false;
            }
            else {
                Schedule[i][1] = SumaAnuiteta;
            }
            TiloFull = TiloFull + Schedule[i][3];
            ProcentFull = ProcentFull + Schedule[i][2];
            WnesokFull = WnesokFull + Schedule[i][1];

            i++;

            if (OstatokKredytu <= 0 || i > StrokKredytu) {
                if (KinecObrahunku) {
                    Schedule.length += 1;
                    Schedule[i] = new Array(5);
                    Schedule[i][0] = "";
                    Schedule[i][1] = WnesokFull;
                    Schedule[i][2] = ProcentFull;
                    Schedule[i][3] = TiloFull;
                    Schedule[i][4] = "";
                }
                break;
            }
        }

        if (KinecObrahunku) {
            break;
        }
        else if ((MaxSumaAnuiteta - MinSumaAnuiteta) <= 0.001) {
            MinSumaAnuiteta = parseFloat(SumaAnuiteta.toFixed(0));
            MaxSumaAnuiteta = parseFloat(SumaAnuiteta.toFixed(0));
            KinecObrahunku = true;

        }
        else if (i > StrokKredytu) {
            MinSumaAnuiteta = SumaAnuiteta;
        }
        else if (i < StrokKredytu) {
            MaxSumaAnuiteta = SumaAnuiteta;
        }
        else if (Schedule[i - 1][1] < SumaAnuiteta) {
            MaxSumaAnuiteta = SumaAnuiteta;
        }
        else {
            MinSumaAnuiteta = SumaAnuiteta;
        }
        count++;
    }

    return Schedule;
}
function ClassicSchedule(SumaKredytu, StrokKredytu, ListStawok) {
    var DataRozrahunku = new Date();
    var DataPo4atkuProc = DataRozrahunku;
    var DataPershogoPlatezhu = AddDays(DataRozrahunku, TotalDays(DataRozrahunku, AddMonths(DataRozrahunku, 1)) - DataRozrahunku.getDate() + 1);
    var DataOplatyKredytu = AddDays(AddMonths(DataRozrahunku, StrokKredytu), -1);
    var Schedule = new Array();
    //var Platizh = [];
    var i = 0; var Tilo = 0;
    var ChyTiloZero = false;
    if (IfBodyIsZero(DataPo4atkuProc, DataPershogoPlatezhu)) {
        ChyTiloZero = true;
        Tilo = (SumaKredytu / StrokKredytu);
    }
    else if (IfIncreaseStrokKredytu(DataRozrahunku, DataPershogoPlatezhu)) {
        Tilo = (SumaKredytu / (StrokKredytu + 1));
    }
    else {
        Tilo = (SumaKredytu / StrokKredytu);
    }
    Tilo = parseFloat(Tilo.toFixed(2));
    if (IfIncreaseStrokKredytu(DataRozrahunku, DataPershogoPlatezhu))
        StrokKredytu = StrokKredytu + 1;

    var DataStart = DataPo4atkuProc;
    var DataEnd = DataPershogoPlatezhu;
    var DataEndTilo = new Date(1900, 0);

    var OstatokKredytu = SumaKredytu;
    var TiloFull = 0; var ProcentFull = 0; var WnesokFull = 0;
    var s = 0;
    var DataZminy = new Date(1900, 0);

    while (true) {

        if (DataEnd.getDate() == 1)
            DataEndTilo = AddDays(DataEnd, (-1));
        else
            DataEndTilo = DataEnd;

        Schedule.length += 1;
        Schedule[i] = new Array(6);

        DataZminy = AddMonths(DataRozrahunku, ListStawok[s][1]);
        var ChyZminaStawky = DataStart < DataZminy && DataEnd > DataZminy && ListStawok.count > s + 1;

        if (DataEndTilo != DataEnd) {

            if (ChyZminaStawky && DataEndTilo > DataZminy) {
                Schedule[i][2] = SumaProcWPeriodi(OstatokKredytu, ListStawok[s][0], DataStart, DataZminy)
                    + SumaProcWPeriodi(OstatokKredytu, ListStawok[s + 1][0], DataZminy, DataEndTilo)
                    + SumaProcWPeriodi(Math.max(OstatokKredytu - Tilo, 0), ListStawok[s + 1][0], DataEndTilo, DataEnd);
                s++;
            }
            else if (ChyZminaStawky) {
                Schedule[i][2] = SumaProcWPeriodi(OstatokKredytu, ListStawok[s][0], DataStart, DataZminy)
                    + SumaProcWPeriodi(Math.max(OstatokKredytu - Tilo, 0), ListStawok[s + 1][0], DataZminy, DataEnd);
                s++;
            }
            else if (ChyTiloZero) {
                Schedule[i][2] = SumaProcWPeriodi(OstatokKredytu, ListStawok[s][0], DataStart, DataEnd);
            }

            else {
                Schedule[i][2] = SumaProcWPeriodi(OstatokKredytu, ListStawok[s][0], DataStart, DataEndTilo)
                    + SumaProcWPeriodi(Math.max(OstatokKredytu - Tilo, 0), ListStawok[s][0], DataEndTilo, DataEnd);
            }
        }
        else {

            if (ChyZminaStawky) {
                Schedule[i][2] = SumaProcWPeriodi(OstatokKredytu, ListStawok[s][0], DataStart, DataZminy)
                    + SumaProcWPeriodi(OstatokKredytu, ListStawok[s + 1][0], DataZminy, DataEnd);
                s++;
            }
            else {
                Schedule[i][2] = SumaProcWPeriodi(OstatokKredytu, ListStawok[s][0], DataStart, DataEnd);
            }

        }
        Schedule[i][2] = parseFloat(Schedule[i][2].toFixed(2));

        if (ChyTiloZero) {
            Schedule[i][3] = 0;
            ChyTiloZero = false;
        }
        else if (Tilo < OstatokKredytu - Tilo / 2)
            Schedule[i][3] = Tilo;
        else
            Schedule[i][3] = OstatokKredytu;

        Schedule[i][4] = OstatokKredytu - Schedule[i][3];
        Schedule[i][5] = DataEnd;
        Schedule[i][0] = FormatDate(DataEnd, 1);
        DataStart = DataEnd;
        if (AddMonths(DataPershogoPlatezhu, (i + 1)) > DataOplatyKredytu)
            DataEnd = DataOplatyKredytu;
        else
            DataEnd = AddMonths(DataPershogoPlatezhu, (i + 1));

        OstatokKredytu = OstatokKredytu - Schedule[i][3];
        Schedule[i][1] = parseFloat((Schedule[i][2] + Schedule[i][3]).toFixed(2));
        TiloFull = TiloFull + Schedule[i][3];
        ProcentFull = ProcentFull + Schedule[i][2];
        WnesokFull = WnesokFull + Schedule[i][1];
        i++;
        if (OstatokKredytu <= 0 || i > StrokKredytu) {
            Schedule.length += 1;
            Schedule[i] = new Array(5);
            Schedule[i][0] = "";
            Schedule[i][1] = WnesokFull;
            Schedule[i][2] = ProcentFull;
            Schedule[i][3] = TiloFull;
            Schedule[i][4] = "";
            break;
        }
    }
    return Schedule;
}
function CalculateEffectiveRate(SumaKredytu, Wytraty, DataRozrahunku, Schedule) {
    SumaKredytu = -1 * SumaKredytu + Wytraty;
    var EffectiveRate = 0.15;
    var Step = EffectiveRate;
    var AmountFlow = new Array(1);
    AmountFlow[0] = new Array(2);
    AmountFlow[0][0] = DataRozrahunku;
    AmountFlow[0][1] = SumaKredytu;
    for (var i = 0; i < Schedule.length - 1; i++) {
        AmountFlow.length += 1;
        AmountFlow[i + 1] = new Array(2);
        AmountFlow[i + 1][0] = Schedule[i][5];
        AmountFlow[i + 1][1] = Schedule[i][1];
    }
    
    while (true) {
        var Drib = 0;
        for (var i = 0; i < AmountFlow.length; i++) {
            
            var dd = TotalDays(DataRozrahunku, AmountFlow[i][0]);
            var Platizh = AmountFlow[i][1];
            Drib += Platizh / Math.pow(1 + EffectiveRate, parseFloat(dd / 365));
        }
        if (parseFloat(Drib.toFixed(1)) == 0)
        {
            break;
        }
        else if (Drib > 0){
            EffectiveRate += Step;
        }
        else {
            Step = Step / 2;
            EffectiveRate -= Step;
        }
    }
    return MaskDecimal(parseFloat((EffectiveRate * 100).toFixed(2)));
}
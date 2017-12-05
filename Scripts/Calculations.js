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
function AnnuitySchedule(SumaKredytu, StrokKredytu, Stawka, Komisiya) {
    var Schedule = new Array();
    var SumaKomisiyi = parseFloat((SumaKredytu * Komisiya / 100).toFixed(2));
    var TiloFull = 0; var ProcentFull = 0; var WnesokFull = 0;
    var OstatokKredytu = SumaKredytu;
    var SumaAnuiteta = parseFloat(Anuitet(SumaKredytu, StrokKredytu, Stawka, Komisiya).toFixed(2));
    var i = 0;
    while (true) {
        Schedule.length += 1;
        Schedule[i] = new Array(4);

        Schedule[i][1] = parseFloat(Procent(OstatokKredytu, Stawka).toFixed(2));
        if (i == StrokKredytu - 1)
            Schedule[i][2] = Schedule[i - 1][3];
        else
            Schedule[i][2] = Math.min(OstatokKredytu, Math.max(SumaAnuiteta - Schedule[i][1] - SumaKomisiyi, 0));
        Schedule[i][3] = OstatokKredytu - Schedule[i][2];
        OstatokKredytu = OstatokKredytu - Schedule[i][2];
        if (OstatokKredytu == 0)
            Schedule[i][0] = parseFloat((Schedule[i][1] + Schedule[i][2] + SumaKomisiyi).toFixed(2));
        else
            Schedule[i][0] = SumaAnuiteta;

        TiloFull = TiloFull + Schedule[i][2];
        ProcentFull = ProcentFull + Schedule[i][1];
        WnesokFull = WnesokFull + Schedule[i][0];

        i++;
        if (OstatokKredytu <= 0 || i == StrokKredytu) {
            Schedule.length += 1;
            Schedule[i] = new Array(4);
            Schedule[i][0] = WnesokFull;
            Schedule[i][1] = ProcentFull;
            Schedule[i][2] = TiloFull;
            Schedule[i][3] = "";
            break;
        }
    }

    return Schedule;
}
function ClassicSchedule(SumaKredytu, StrokKredytu, Stawka) {
    var Schedule = new Array();
    var i = 0;
    var Tilo = parseFloat((SumaKredytu / StrokKredytu).toFixed(2));
    var OstatokKredytu = SumaKredytu;
    var TiloFull = 0; var ProcentFull = 0; var WnesokFull = 0;
    while (true) {
        Schedule.length += 1;
        Schedule[i] = new Array(4);
        Schedule[i][1] = parseFloat(Procent(OstatokKredytu, Stawka).toFixed(2));
        if (Tilo < OstatokKredytu - Tilo / 2)
            Schedule[i][2] = Tilo;
        else
            Schedule[i][2] = OstatokKredytu;
        Schedule[i][3] = OstatokKredytu - Schedule[i][2];
        OstatokKredytu = OstatokKredytu - Schedule[i][2];
        Schedule[i][0] = parseFloat((Schedule[i][1] + Schedule[i][2]).toFixed(2));
        TiloFull = TiloFull + Schedule[i][2];
        ProcentFull = ProcentFull + Schedule[i][1];
        WnesokFull = WnesokFull + Schedule[i][0];
        i++;
        if (OstatokKredytu <= 0 || i == StrokKredytu) {
            Schedule.length += 1;
            Schedule[i] = new Array(4);
            Schedule[i][0] = WnesokFull;
            Schedule[i][1] = ProcentFull;
            Schedule[i][2] = TiloFull;
            Schedule[i][3] = "";
            break;
        }
    }
    return Schedule;
}
function CalculateEffectiveRate(SumaKredytu, Wytraty, DataRozrahunku, Schedule) {
    DataRozrahunku = new Date();
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
        AmountFlow[i + 1][0] = AddMonths(DataRozrahunku, i + 1);
        AmountFlow[i + 1][1] = Schedule[i][0];
    }

    while (true) {
        var Drib = 0;
        for (var i = 0; i < AmountFlow.length; i++) {

            var dd = TotalDays(DataRozrahunku, AmountFlow[i][0]);
            var Platizh = AmountFlow[i][1];
            Drib += Platizh / Math.pow(1 + EffectiveRate, parseFloat(dd / 365));
        }
        if (parseFloat(Drib.toFixed(1)) == 0) {
            break;
        }
        else if (Drib > 0) {
            EffectiveRate += Step;
        }
        else {
            Step = Step / 2;
            EffectiveRate -= Step;
        }
    }
    return MaskDecimal(parseFloat((EffectiveRate * 100).toFixed(2)));
}
function Anuitet(CK, T, stawka, komisiya) {
    if (typeof (CK) != "number")
        CK = parseFloat(CK.replace(',', '.'));
    if (typeof (T) != "number")
        T = parseInt(T);
    if (typeof (stawka) != "number")
        stawka = parseFloat(stawka.replace(',', '.'));
    if (typeof (komisiya) != "number")
        komisiya = parseFloat(komisiya.replace(',', '.'));
    var stepin = Math.pow(1 + stawka / 1200, T);
    var drib = stawka * stepin / 1200 / (stepin - 1);
    return CK * (drib + komisiya / 100);
}
function Procent(CK, stawka) {
    if (typeof (CK) != "number")
        CK = parseFloat(CK.replace(',', '.'));
    if (typeof (stawka) != "number")
        stawka = parseFloat(stawka.replace(',', '.'));
    return CK * stawka * 30 / 36000;
}
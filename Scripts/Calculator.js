function GenerateTable(Schedule) {
    var result = "<table>";
    result += "<tr>";
    result += "<th>№</th>";
    result += "<th>Дата оплати до</th>";
    result += "<th>Платіж</th>";
    result += "<th>Проценти</th>";
    result += "<th>Погашення тіла</th>";
    result += "<th>Залишок</th>";
    result += "</tr>";
    for (var i = 0; i < Schedule.length - 1; i++) {
        result += "<tr>";
        result += "<td>" + (i + 1) + "</td>";
        for (var j = 0; j < Schedule[i].length - 1; j++) {
            if (j >= 1)
                result += "<td>" + MaskDecimal(Schedule[i][j]) + "</td>";
            else
                result += "<td>" + Schedule[i][j] + "</td>";
        }
        result += "</tr>";
    }
    result += "</table>";
    return result;
}
function Calaculate() {
    if (classic.checked)
        a = ClassicSchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok);
    else
        a = AnnuitySchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok, 0);

    SumaPlatezhiv.innerHTML = MaskDecimal(a[a.length - 1][1]);
    SumaProcentiv.innerHTML = MaskDecimal(a[a.length - 1][2]);
    Schedule.innerHTML = GenerateTable(a);

    var CalculatedRealRate = parseFloat((a[a.length - 1][2] + SumaStrahovky + SumaKomisiyiZaVydachu) * 100 * 12 / (parseFloat(Suma.value) * parseFloat(Termin.value))).toFixed(2);
    RealRate.innerHTML = MaskDecimal(CalculatedRealRate) + "%";
    var CalculatedRealRateFull = parseFloat((a[a.length - 1][2] + SumaStrahovky + SumaKomisiyiZaVydachu) * 100 / parseFloat(Suma.value)).toFixed(2);
    RealRateFull.innerHTML = MaskDecimal(CalculatedRealRateFull) + "%";
}

var ListStawok = new Array(1);
ListStawok[0] = new Array(2);
ListStawok[0][0] = 15.9;
ListStawok[0][1] = 1000;
var a;
var ProcentStrahowky = 0.6;
var ProcentKomisiyiZaVydachu = 0.99;

outputTermin.value = MaskInt(Termin.value);
outputSuma.value = MaskInt(Suma.value);

var SumaStrahovky = parseFloat((parseFloat(Suma.value) * ProcentStrahowky / 100).toFixed(2));
var SumaKomisiyiZaVydachu = parseFloat((parseFloat(Suma.value) * ProcentKomisiyiZaVydachu / 100).toFixed(2));
Strahovka.innerHTML = MaskDecimal(SumaStrahovky);
KomisiyaZaVydachu.innerHTML = MaskDecimal(SumaKomisiyiZaVydachu);

a = AnnuitySchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok, 0);
Calaculate();

outputTermin.onchange = function () {
    Termin.value = (this).value;
    Termin.oninput();
}

outputTermin.oninput = function () {
    if (this.value == '')
        outputTermin.value = Termin.min;
    else if (parseInt(this.value) > parseInt(Termin.max))
        outputTermin.value = Termin.max;
    else
        outputTermin.value = this.value;

    Termin.value = outputTermin.value;
}

outputSuma.onchange = function () {
    
    Suma.value = (this.value).replace(/ /g, '');
    Suma.oninput();
}

outputSuma.oninput = function () {
    var n;
    if (this.value != '')
        n = parseInt((this.value).replace(/ /g, ''));
    else
        n = parseInt((Suma.min).replace(/ /g, ''));

    outputSuma.value = MaskInt(n);
    Suma.value = n;
}

Termin.oninput = function () {
    outputTermin.value = MaskInt(this.value);
    Calaculate();
}

Suma.oninput = function () {
    outputSuma.value = MaskInt(this.value);

    SumaStrahovky = parseFloat((parseFloat(this.value) * ProcentStrahowky / 100).toFixed(2));
    SumaKomisiyiZaVydachu = parseFloat((parseFloat(this.value) * ProcentKomisiyiZaVydachu / 100).toFixed(2));
    Strahovka.innerHTML = MaskDecimal(SumaStrahovky);
    KomisiyaZaVydachu.innerHTML = MaskDecimal(SumaKomisiyiZaVydachu);
    Calaculate();
}

annuity.onchange = function () {
    Calaculate();
}


classic.onchange = function () {
    Calaculate();
}


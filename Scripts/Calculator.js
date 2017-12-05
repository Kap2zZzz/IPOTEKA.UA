function GenerateTable(Schedule) {
    var result = "<table>";
    result += "<tr>";
    result += "<th>Місяць</th>";
    result += "<th>Платіж</th>";
    result += "<th>Проценти</th>";
    result += "<th>Погашення тіла</th>";
    result += "<th>Залишок</th>";
    result += "</tr>";
    for (var i = 0; i < Schedule.length - 1; i++) {
        result += "<tr>";
        result += "<td>" + (i + 1) + "</td>";
        for (var j = 0; j < Schedule[i].length; j++) {
            result += "<td>" + MaskDecimal(Schedule[i][j]) + "</td>";
        }
        result += "</tr>";
    }
    result += "</table>";
    return result;
}
function Calculate() {
    ListStawok = parseFloat(NominalRate.value.replace(",", "."));

    ProcentKomisiyiZaVydachu = parseFloat(KomisiyaZaVydachuIn.value.replace(",", "."));

    if (classic.checked)
        a = ClassicSchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok);
    else
        a = AnnuitySchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok, 0);

    SumaKomisiyiZaVydachu = parseFloat((parseFloat(Suma.value) * ProcentKomisiyiZaVydachu / 100).toFixed(2));
    CalculatedRealRate = parseFloat((a[a.length - 1][1] + SumaKomisiyiZaVydachu) * 100 * 12 / (parseFloat(Suma.value) * parseFloat(Termin.value))).toFixed(2);
    RealRate.innerHTML = MaskDecimal(CalculatedRealRate) + "%";
    CalculatedRealRateFull = parseFloat((a[a.length - 1][1] + SumaKomisiyiZaVydachu) * 100 / parseFloat(Suma.value)).toFixed(2);

    SumaPlatezhiv.innerHTML = MaskDecimal(a[a.length - 1][0]);
    SumaProcentiv.innerHTML = MaskDecimal(a[a.length - 1][1]) + " (" + MaskDecimal(CalculatedRealRateFull) + "%)";
    KomisiyaZaVydachu.innerHTML = MaskDecimal(SumaKomisiyiZaVydachu) + " (";
    KomisiyaZaVydachuAfter.innerHTML = ")%"
    EffectiveRate.innerHTML = CalculateEffectiveRate(parseInt(Suma.value), SumaKomisiyiZaVydachu, new Date(), a) + "%";
}

var a;
var ListStawok;
var ProcentKomisiyiZaVydachu;
var CalculatedRealRate;
var CalculatedRealRateFull;
var KomisiyaZaWydachuTemp = KomisiyaZaVydachuIn.value;
var NominalRateTemp = NominalRate.value;

outputTermin.value = Termin.value;
outputSuma.value = MaskInt(Suma.value);

Calculate();
Schedule.innerHTML = GenerateTable(a);

outputTermin.onchange = function () {
    Termin.value = this.value;
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
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
    if (this.value != '') {
        Suma.value = this.value.replace(/ /g, '');
        if (parseInt(this.value.replace(/ /g, '')) < parseInt(Suma.min))
            outputSuma.value = MaskInt(Suma.min);
        else if (parseInt(this.value.replace(/ /g, '')) > parseInt(Suma.max))
            outputSuma.value = MaskInt(Suma.max);
    }
    else {
        Suma.value = Suma.min;
        this.value = MaskInt(Suma.min);
    }
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}

outputSuma.oninput = function () {
    if (this.value != '') {
        if (this.value.charAt(this.value.length - 1) == ' ' || this.value.match(/[1-9][\d ]*/g) != this.value)
            this.value = this.value.substr(0, this.value.length - 1);
        else {
            this.value = this.value.replace(/ /g, '');
            Suma.value = this.value;
            this.value = MaskInt(this.value);
        }
    }
    else
        Suma.value = Suma.min;
}

Termin.oninput = function () {
    outputTermin.value = MaskInt(this.value);
    Calculate();
}

Termin.onchange = function () {
    Calculate();
    Schedule.innerHTML = GenerateTable(a);

}

Suma.oninput = function () {
    outputSuma.value = MaskInt(this.value);
    Calculate();
}

Suma.onchange = function () {
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}

annuity.onchange = function () {
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}


classic.onchange = function () {
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}

KomisiyaZaVydachuIn.oninput = function () {
    this.value = this.value.replace('.', ',');
    if (this.value != this.value.match(/^(\d?|[1-9]\d|[1-9]?\d,\d{0,2})$/g))
        this.value = KomisiyaZaWydachuTemp;
    KomisiyaZaWydachuTemp = this.value;
}

NominalRate.oninput = function () {
    this.value = this.value.replace('.', ',');
    if (this.value != this.value.match(/^(\d?|[1-9]\d|[1-9]?\d,\d{0,2})$/g))
        this.value = NominalRateTemp;
    NominalRateTemp = this.value;
}

KomisiyaZaVydachuIn.onchange = function () {
    if (this.value == '')
        this.value = MaskDecimal(0);
    else
        this.value = MaskDecimal(parseFloat(this.value.replace(',', '.')));
    if (parseFloat(this.value.replace(',', '.')) > 50)
        this.value = MaskDecimal(50);
    Calculate();
    KomisiyaZaWydachuTemp = this.value;
}

NominalRate.onchange = function () {
    if (this.value == '' || parseFloat(this.value.replace(',', '.')) == 0)
        this.value = MaskDecimal(0.01);
    else
        this.value = MaskDecimal(parseFloat(this.value.replace(',', '.')));
    NominalRateTemp = this.value;
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}
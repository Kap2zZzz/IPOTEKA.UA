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
function Calculate() {
    ListStawok = new Array(1);
    ListStawok[0] = new Array(2);
    ListStawok[0][0] = parseFloat(NominalRate.value.replace(",", "."));
    ListStawok[0][1] = 1000;

    ProcentKomisiyiZaVydachu = parseFloat(KomisiyaZaVydachuIn.value.replace(",", "."));

    if (classic.checked)
        a = ClassicSchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok);
    else
        a = AnnuitySchedule(parseFloat(Suma.value), parseInt(Termin.value), ListStawok, 0);

    SumaKomisiyiZaVydachu = parseFloat((parseFloat(Suma.value) * ProcentKomisiyiZaVydachu / 100).toFixed(2));
    CalculatedRealRate = parseFloat((a[a.length - 1][2] + SumaKomisiyiZaVydachu) * 100 * 12 / (parseFloat(Suma.value) * parseFloat(Termin.value))).toFixed(2);
    RealRate.innerHTML = MaskDecimal(CalculatedRealRate) + "%";
    CalculatedRealRateFull = parseFloat((a[a.length - 1][2] + SumaKomisiyiZaVydachu) * 100 / parseFloat(Suma.value)).toFixed(2);

    SumaPlatezhiv.innerHTML = MaskDecimal(a[a.length - 1][1]);
    SumaProcentiv.innerHTML = MaskDecimal(a[a.length - 1][2]) + " (" + MaskDecimal(CalculatedRealRateFull) + "%)";
    KomisiyaZaVydachu.innerHTML = MaskDecimal(SumaKomisiyiZaVydachu) + " (";
    KomisiyaZaVydachuAfter.innerHTML = ")%"
    EffectiveRate.innerHTML = CalculateEffectiveRate(parseInt(Suma.value), SumaKomisiyiZaVydachu, new Date(), a) + "%";
}

var a;
var ListStawok;
var ProcentKomisiyiZaVydachu;
var CalculatedRealRate;
var CalculatedRealRateFull;

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
    if (this.value != this.value.match(/^(\d|[1-9]\d|[1-9]?\d,\d{0,2})$/g))
        this.value = this.value.substr(0, this.value.length - 1);
}

NominalRate.oninput = function () {
    this.value = this.value.replace('.', ',');
    if (this.value != this.value.match(/^(\d|[1-9]\d|[1-9]?\d,\d{0,2})$/g))
        this.value = this.value.substr(0, this.value.length - 1);
}

KomisiyaZaVydachuIn.onchange = function () {
    this.value = MaskDecimal(parseFloat(this.value.replace(',', '.')));
    Calculate();
}

NominalRate.onchange = function () {
    this.value = MaskDecimal(parseFloat(this.value.replace(',', '.')));
    Calculate();
    Schedule.innerHTML = GenerateTable(a);
}

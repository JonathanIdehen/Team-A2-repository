## Use Case 1:
**Titel:** Registrer indgangskontrol

**Primær aktør:** Medarbejder

**Scope:** Kvalitetssystem

**Hovedscenarie:**
1. Medarbejder vælger “Registrer indgangskontrol”
2. Medarbejder registrerer bremsekaliber ID
3. Systemet viser bremsekaliber-typen
4. Medarbejder godkender bremsekaliber-typen
5. Medarbejder registrerer dato for indgangskontrol
6. Medarbejder vælger at gemme
7. Systemet gemmer indgangskontrollen for bremsekaliberen.


## Use Case 2:

**Titel:** Registrer slutkontrol

**Primær aktør:** Medarbejder

**Scope:** Kvalitetssystem

**Hovedscenarie:**

1. Medarbejder vælger “Registrer slutkontrol”.
2. Medarbejder registrerer bremsekaliber ID.
3. Systemet viser bremsekaliber-typen.
4. Medarbejder godkender bremsekaliber-typen.
5. Systemet beder om dato, spild og afgang.
6. Medarbejder registrerer dato, spild, afgang og godkender.

   6a (Hvis bremsekaliber ikke skal godkendes)
   
   1. Medarbejder vælger “Godkend ikke”.
   2. Systemet viser en liste over potentielle fejlårsager.
   3. Medarbejder vælger fejlårsag.
   4. Systemet gemmer og markerer bremsekaliberen som “Ikke godkendt”.
7. Systemet gemmer og markerer bremsekaliberen som godkendt.


## Use Case 3:

**Titel:** Få historik for bremsekalibre

**Primær aktør:** Medarbejder

**Scope:** Kvalitetssystem

**Hovedscenarie:**

1. Medarbejder vælger “Få historik for bremsekalibre”
2. Medarbejder registrerer bremsekaliber ID
3. Systemet viser historikken for bremsekaliberen.




### Fully dressed - Use Case 2:

**Titel:** Registrer slutkontrol

**Primær aktør:** Medarbejder

**Scope:** Kvalitetssystem

**User level:** At registrere en slutkontrol

**Preconditions:**

- Bremsekaliberen er registreret i systemet.

- Renoveringsprocessen er afsluttet.

**Succes guarantee:**

- Slutkontrollen er registreret korrekt.

- Bremsekaliberens status er opdateret (godkendt, rework, eller smide ud)

**Hovedscenarie:**

1. Medarbejder vælger “Registrer slutkontrol”.
2. Medarbejder registrerer bremsekaliber ID.
3. Systemet viser bremsekaliber-typen.
4. Medarbejder godkender bremsekaliber-typen.
5. Systemet beder om dato, spild og afgang.
6. Medarbejder registrerer dato, spild, afgang og godkender.

   6a (Hvis bremsekaliber ikke skal godkendes)
   1. Medarbejder vælger “Godkend ikke”.
   2. Systemet viser en liste over potentielle fejlårsager.
   3. Medarbejder vælger fejlårsag.
   4. Systemet gemmer og markerer bremsekaliberen som “Ikke godkendt”.
7. Systemet gemmer og markerer bremsekaliberen som godkendt.
# Actividad de apredizaje 2
# ?? Gestor de Biblioteca - Proyecto en C# con Pruebas Unitarias

Este proyecto consiste en implementar una clase `LibraryManager` para administrar libros en una biblioteca digital. Se incluye una interfaz de consola para interactuar con el sistema y un conjunto de pruebas unitarias desarrolladas con NUnit.

---

## ?? Objetivo

Desarrollar una clase en C# que cumpla con los siguientes requerimientos funcionales:

---


##  ? Casos de Prueba Unitarios - `LibraryManager`

A continuación se detallan los escenarios de prueba organizados por funcionalidad.

---

## 1. ?? AddBook

| ID    | Descripción                         | Datos de Entrada                                                | Resultado Esperado                                               |
|-------|-------------------------------------|------------------------------------------------------------------|------------------------------------------------------------------|
| TC1.1 | Agregar un libro con datos válidos  | `"Cien Años de Soledad", "Gabriel García Márquez", "1111", 1967` | El libro se agrega exitosamente                                 |
| TC1.2 | Agregar un libro con ISBN duplicado | `"Nuevo Libro", "Otro Autor", "1111", 2023`                      | Se muestra mensaje de error, no se agrega                       |
| TC1.3 | Agregar libro con año inválido      | `"Test", "Autor", "9999", -2020`                                 | Debería rechazarse, pero actualmente no hay validación activa   |

?? **Mejora sugerida:** Validar que el año sea positivo.

---

## 2. ? RemoveBook

| ID    | Descripción             | Datos de Entrada | Resultado Esperado                                |
|-------|-------------------------|------------------|---------------------------------------------------|
| TC2.1 | Eliminar libro existente| `"1111"`         | Libro eliminado exitosamente                      |
| TC2.2 | Eliminar libro inexistente | `"0000"`      | Mensaje de error, sin afectar la colección        |

---

## 3. ?? SearchByTitle

| ID    | Descripción             | Datos de Entrada | Resultado Esperado                                |
|-------|-------------------------|------------------|---------------------------------------------------|
| TC3.1 | Buscar título exacto    | `"1984"`         | Devuelve lista con libro `"1984"`                 |
| TC3.2 | Buscar título parcial   | `"soledad"`      | Devuelve lista con `"Cien Años de Soledad"`       |
| TC3.3 | Buscar título inexistente| `"Harry Potter"`| Devuelve lista vacía                              |

---

## 4. ?? ListBooks

| ID    | Descripción               | Estado Inicial            | Resultado Esperado                     |
|-------|---------------------------|----------------------------|----------------------------------------|
| TC4.1 | Listar con libros cargados| Múltiples libros cargados  | Lista completa mostrada                |
| TC4.2 | Listar sin libros         | Colección vacía            | Mensaje `"No hay libros..."`           |

---

## 5. ?? IsAvailable

| ID    | Descripción         | Estado del libro           | Resultado Esperado |
|-------|---------------------|-----------------------------|--------------------|
| TC5.1 | Libro disponible     | `"1984"` no prestado        | Retorna `true`     |
| TC5.2 | Libro prestado       | `"1984"` prestado           | Retorna `false`    |
| TC5.3 | Libro inexistente    | `"0000"`                    | Retorna `false`    |

---

## 6. ?? BorrowBook

| ID    | Descripción               | Estado Inicial       | Resultado Esperado                                         |
|-------|---------------------------|-----------------------|-------------------------------------------------------------|
| TC6.1 | Prestar libro disponible  | `"1984"` no prestado  | `IsBorrowed = true`, mensaje de éxito                      |
| TC6.2 | Prestar libro ya prestado | `"1984"` prestado     | Mensaje `"El libro ya está prestado"`                      |
| TC6.3 | Prestar libro inexistente | `"9999"`              | Mensaje `"Libro no encontrado"`                            |

---

## 7. ?? ReturnBook

| ID    | Descripción                | Estado Inicial      | Resultado Esperado                                         |
|-------|----------------------------|----------------------|-------------------------------------------------------------|
| TC7.1 | Devolver libro prestado    | `"1984"` prestado    | `IsBorrowed = false`, mensaje de éxito                     |
| TC7.2 | Devolver libro no prestado | `"1984"` disponible  | Mensaje `"El libro ya estaba disponible"`                 |
| TC7.3 | Devolver libro inexistente | `"0000"`             | Mensaje `"Libro no encontrado"`                            |



---
## Explicacion de las pruebas 
### ? 1. Agregar libros (AddBook)
Verifica que se puedan agregar libros correctamente, que no se permita duplicados, y que aún falta validación para años inválidos.

```csharp
[Test]
public void TC1_1_AddBook_ValidData_BookAdded() { ... }

[Test]
public void TC1_2_AddBook_DuplicateISBN_ShowsErrorAndDoesNotAdd() { ... }

[Test]
public void TC1_3_AddBook_InvalidYear_NoValidationPresent() { ... }


```
### ? 2. Eliminar libros (RemoveBook)
Confirma que se pueden eliminar libros existentes y se muestra un mensaje adecuado si el ISBN no se encuentra. 
```csharp
[Test]
public void TC2_1_RemoveBook_ExistingBook_RemovesSuccessfully() { ... }

[Test]
public void TC2_2_RemoveBook_NonExistent_ShowsError() { ... }
[Test]
public void TC2_1_RemoveBook_ExistingBook_RemovesSuccessfully() { ... }

[Test]
public void TC2_2_RemoveBook_NonExistent_ShowsError() { ... }

```
###  3. Buscar por título (SearchByTitle)
Valida que se puedan buscar libros por coincidencia exacta o parcial, y que no devuelva resultados si no hay coincidencias.
```csharp
[Test]
public void TC3_1_SearchByTitle_ExactMatch_FindsBook() { ... }

[Test]
public void TC3_2_SearchByTitle_PartialMatch_FindsBook() { ... }

[Test]
public void TC3_3_SearchByTitle_NoMatch_ReturnsEmpty() { ... }

```
### 4. Listar libros (ListBooks)
Evalúa que se pueda listar correctamente la colección de libros, y se muestre un mensaje si está vacía.

```csharp
[Test]
public void TC4_1_ListBooks_WithBooks_DisplaysAll() { ... }

[Test]
public void TC4_2_ListBooks_EmptyCollection_ShowsMessage() { ... }

```
### ?? 5. Verificar disponibilidad (IsAvailable)
Asegura que se pueda verificar si un libro está disponible, prestado o inexistente.
```csharp
[Test]
public void TC5_1_IsAvailable_BookNotBorrowed_ReturnsTrue() { ... }

[Test]
public void TC5_2_IsAvailable_BookBorrowed_ReturnsFalse() { ... }

[Test]
public void TC5_3_IsAvailable_BookDoesNotExist_ReturnsFalse() { ... }

```
### ?? 6. Prestar libros (BorrowBook)
Valida que se pueda prestar un libro disponible, y muestra mensajes correctos si ya fue prestado o no existe.
```csharp
[Test]
public void TC6_1_BorrowBook_AvailableBook_Success() { ... }

[Test]
public void TC6_2_BorrowBook_AlreadyBorrowed_ShowsMessage() { ... }

[Test]
public void TC6_3_BorrowBook_NonExistentBook_ShowsMessage() { ... }

```
### 7. Devolver libros (ReturnBook)
Confirma que se puede devolver correctamente un libro prestado, y da mensajes adecuados si ya estaba disponible o no existe.
```csharp
[Test]
public void TC7_1_ReturnBook_BookWasBorrowed_Success() { ... }

[Test]
public void TC7_2_ReturnBook_BookWasNotBorrowed_ShowsMessage() { ... }

[Test]
public void TC7_3_ReturnBook_BookDoesNotExist_ShowsMessage() { ... }

```
#### ? Total de pruebas cubiertas: 19
#### ?? Funciones cubiertas: Agregar, Eliminar, Buscar, Listar, Verificar disponibilidad, Prestar, Devolver

Para indagar mas sobre el codigo y documentacion de las pruebas puedes vistar el Github de este proyecto
[Gestor Biblioteca](https://github.com/CristiamVM/GestorBiblioteca/tree/master)

---
## Resultados de los casos de preubas
| ID    | Descripción                 | Entrada                     | Resultado Esperado              |
| ----- | --------------------------- | --------------------------- | ------------------------------- |
| TC1.1 | Agregar libro válido        | Título, Autor, "1111", 1967 | Se agrega correctamente         |
| TC1.2 | ISBN duplicado              | "1111"                      | Error por ISBN duplicado        |
| TC1.3 | Año negativo                | -2020                       | No se valida (Mejora pendiente) |
| TC2.1 | Eliminar libro existente    | "1111"                      | Se elimina con éxito            |
| TC2.2 | Eliminar libro inexistente  | "0000"                      | Error: no se encuentra el libro |
| TC3.1 | Buscar título exacto        | "1984"                      | Devuelve libro correcto         |
| TC3.2 | Buscar título parcial       | "soledad"                   | Devuelve "Cien Años de Soledad" |
| TC3.3 | Buscar título inexistente   | "Harry Potter"              | Lista vacía                     |
| TC4.1 | Listar con libros cargados  | -                           | Lista completa mostrada         |
| TC4.2 | Listar sin libros           | -                           | Mensaje de biblioteca vacía     |
| TC5.1 | Verificar disponibilidad    | Libro disponible            | true                            |
| TC5.2 | Verificar no disponibilidad | Libro prestado              | false                           |
| TC5.3 | Verificar libro inexistente | "0000"                      | false                           |
| TC6.1 | Prestar libro disponible    | "1984"                      | Prestado con éxito              |
| TC6.2 | Prestar libro ya prestado   | "1984"                      | Mensaje: ya está prestado       |
| TC6.3 | Prestar libro inexistente   | "9999"                      | Mensaje: no encontrado          |
| TC7.1 | Devolver libro prestado     | "1984"                      | Devuelto correctamente          |
| TC7.2 | Devolver libro no prestado  | "1984"                      | Mensaje: ya estaba disponible   |
| TC7.3 | Devolver libro inexistente  | "0000"                      | Mensaje: no encontrado          |

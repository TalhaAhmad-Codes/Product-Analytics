
---

# 📝 Logs

---


## 🛢️ Database

| Attribute | Datatype | Nullable | Constraint |
|-----------|----------|----------|------------|
| Id | Integer | `No` | Primary Key |
| Product Id | Integer | `No` | Foriegn Key("Products") |
| Sell Date | Date | `No` | --- |
| Quantity | Integer | `No` | --- |
| Created At | Data Time | `No` | --- |

> **Unique:** Log of same *Product Id* and *Sell Date* can only be added at once.

---

## 📋 DTOs

---

### 🔍 GET

#### Filter (Pagination)

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Min Quantity | Decimal | `Yes` |
| Max Quantity | Decimal | `Yes` |
| From Sell Date | Date | `Yes` |
| To Sell Date | Date | `Yes` |
| Product Id | Integer | `Yes` |
| Page Number | Integer | `No` |
| Page Size | Integer | `No` |

#### By Id

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |

---

### ➕ POST

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Product Id | Integer | `No` |
| Sell Date | Date | `No` |
| Quantity | Integer | `No` |

---

### 🔄 PUT

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |
| Product Id | Integer | `No` |
| Sell Date | Date | `No` |
| Quantity | Integer | `No` |

---

### 🗑️ DELETE

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |

---

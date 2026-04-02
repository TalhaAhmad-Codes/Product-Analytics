
---

# 📦 Products

---


## 🛢️ Database

| Attribute | Datatype | Nullable | Constraint |
|-----------|----------|----------|------------|
| Id | Integer | `No` | Primary Key |
| Image | Byte-Array | `No` | --- |
| Name | String | `No` | Unique |
| Price | Decimal | `No` | --- |
| Stock Status | Enum('Instock', 'Out of stock') | `No` | --- |
| Created At | Data Time | `No` | --- |

---

## 📋 DTOs

---

### 🔍 GET

#### Filter (Pagination)

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Stock Status | Enum |  `Yes` |
| Min Price | Decimal | `Yes` |
| Max Price | Decimal | `Yes` |
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
| Image | Byte-Array | `No` |
| Name | String | `No` |
| Price | Decimal | `No` |
| Stock Status | Enum('Instock', 'Out of stock') | `No` |

---

### 🔄 PUT

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |
| Image | Byte-Array | `No` |
| Name | String | `No` |
| Price | Decimal | `No` |
| Stock Status | Enum('Instock', 'Out of stock') | `No` |

---

### 🗑️ DELETE

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |

---

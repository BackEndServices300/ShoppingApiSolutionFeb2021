# Products Resource

## GET /products

Allows the consumer to get a list of products

### Responses

##### 200 Ok

```
{
    "data": [
        { id: 39, "name": "Ketchup", "price": 3.99 }


    ]
}
```

## GET /products/{id}

Allows the consumer to get a single product.

### Responses

#### 400 Not Found

#### 200 Ok

```
    {
        "id": 1,
        "name": "Taco Shells",
        "price": 2.99,
        "numberInInventory": 8,
        "daysInInventory": 378
    }

```
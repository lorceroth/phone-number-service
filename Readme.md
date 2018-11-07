# Phone number service

This service provides a way to format and validate swedish mobile phone numbers.

There are two endpoints, ```/format``` and ```/validate```.

## Format

This endpoint accepts a DTO with a ```number```-property. The string property can be any valid phone number. If the input is valid, the service returns a DTO with a ```number```-property, with a formatted value.

### Example

Input
```
{
    "number": "+46712312415"
}
```

Output
```
{
    "number": "+46 712 31 24 15"
}
```

## Validate

This endpoint accepts a DTO with a ```number```-property. The string property can be any valid phone number. If the input is valid, the service returns a DTO with a ```valid```-property, with a boolean value.

### Example

Input
```
{
    "number": "+46 712312415"
}
```

Output
```
{
    "valid": false
}
```


## Error handling

If the input string is in an incorrect format, eg. contains invalid characters, the HTTP status code BadRequest is returned from the endpoints.

## Assignment

Your assignment is to implement a simple Api with controllers and corresponding unit test, satisfying the integration tests in place.

1. Start by forking the repository.
2. Clone the fork to your local machine.
3. Implement a solution, committing your changes along the way.
4. Make sure the tests are passing.
5. Create a pull request from the new fork (https://help.github.com/articles/creating-a-pull-request-from-a-fork/).


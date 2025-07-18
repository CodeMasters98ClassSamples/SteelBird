﻿using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SteelBird.Application.Wrappers;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "This is an Error");
}

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException();

            case false when error == Error.None:
                throw new InvalidOperationException();

            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public HttpStatusCode Status { get; set; }
    public string Message { get; set; }
    public Result WithMessage(string message)
    {
        Message = message;
        return this;
    }

    //With out Generic mode
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result Failure() => new(false, new Error("500","Default Error"));

    //Generic Mode
    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    public static Result<T> Failure<T>(Error error) => new(default, false, error);

    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(Error.NullValue);
}


public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
        => _value = value;

    [NotNull]
    public T Value => _value! ?? throw new InvalidOperationException("Result has no value");

    public static implicit operator Result<T>(T? value) => Create(value);
}
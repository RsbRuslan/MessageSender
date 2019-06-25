using System;
using LiteDB;

namespace MessageSender.Interfaces
{
    public interface IDbService<TDto>
    {
        //void Invoke<TCollection>(Action<LiteDatabase, LiteCollection<TCollection>> action);
        //TReturn Invoke<TReturn, TCollection>(Func<LiteDatabase, LiteCollection<TCollection>, TReturn> action);

        void Invoke(Action<LiteDatabase, LiteCollection<TDto>> action);
        TReturn Invoke<TReturn>(Func<LiteDatabase, LiteCollection<TDto>, TReturn> action);
    }
}
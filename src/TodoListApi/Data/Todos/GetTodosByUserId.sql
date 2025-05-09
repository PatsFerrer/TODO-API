 SELECT 
       Id,
       Title,
       IsCompleted,
       CreatedAt,
       UserId
   FROM 
       Todos
   WHERE 
       UserId = @UserId;
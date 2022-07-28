open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Microsoft.AspNetCore.Http

module MessageModel =
    type Message =
        {
            id : int
            Text : string
        }


module MessageController =
    open MessageModel
    let messageHandler(text:string) = htmlString ("<h1>" + text + "</h1>")

    let messageJsonHandler(text:string) = json { id = 1; Text = text }

    let messageNameTextHandler(name:string,text:string) = 
        htmlString ($@"
            <h1>{name}</h1>
            <p>{text}</p>")

    let routes() = choose [
        routef "/text/%s" text
        routef "/messages/%s" messageHandler
        routef "/message/json/%s" messageJsonHandler
        routef "/messagenametext/%s/%s" messageNameTextHandler]


module UserModel =
    type User = {
        user_id : int
        login : string
        password : string
        verified : bool
        gender : int
        age : int
        user_type : int
    }
    type Personal = {
        person_id : int
        user_id : int
        name : string
        surname : string
        father_name : string
        post : int
    }
    type Client = {
        client_id : int
        user_id : int
        name : string
        surname : string
        father_name : string
        money : int
        client_type : int
    }

module UserController =
    let getUserListHandler() =
        //some operations
        text "List of users"

    let getUserByIdHandler(user_id:int) =
        //some operations
        text "John Doe"

    let deleteUserById(user_id:int) = //Надоело писать Handler в названии функции
        text "User was deleted"

    open UserModel
    let getUserListJson() =
        let John={
            user_id = 1
            login = "johndoe"
            password = "admin"
            verified = true
            gender = 1
            age = 25
            user_type = 1}
        let Adelina={
            user_id = 2
            login = "adelinadoe"
            password = "admin"
            verified = true
            gender = 2
            age = 20
            user_type = 1}
        json [John; Adelina]

    let checkJsonMethod() =
        let John={
            user_id = 1
            login = "johndoe"
            password = "admin"
            verified = true
            gender = 1
            age = 25
            user_type = 1}
        json (
            John,
            [1..10],
            seq{11..20},
            [|"one","two"|],
            true,25,
            "Hahaha")




    [<CLIMutable>]
    type userId = { userId:int }
    let getUserDataById() =

        let a=fun(ctx:HttpContext)->
            async{
                //let! id = ctx.BindModelAsync<userId>()
                //ВОТ ЗДЕСЬ НЕПОНЯТНО КАК БРАТЬ
                0
            }
        let res = bindForm<{|userId:int|}> 
        json {
            user_id = 1
            login = "johndoe"
            password = "admin"
            verified = true
            gender = 1
            age = 25
            user_type = 1
        }

    let changeUserDataById(userId, login, password, age) =
        json {
            user_id = userId
            login = login
            password = password
            verified = true
            gender = 1
            age = age
            user_type = 1
        }

    let routes() = choose [
        route "/users" >=> getUserListHandler()
        routef "/users/%i" getUserByIdHandler
        routef "/users/delete/%i" deleteUserById
        POST >=> choose 
            [
            route "/api/users" >=> getUserListJson()
            route "/api/checkjson" >=> checkJsonMethod()
            route "/api/user" >=> getUserDataById()
            ]
        ]





let webApp() = choose [
        route "/index"  >=> htmlFile "/index.html"
        MessageController.routes()
        UserController.routes()]

let configureApp (app : IApplicationBuilder) =
    // Add Giraffe to the ASP.NET Core pipeline
    app.UseGiraffe (webApp())

let configureServices (services : IServiceCollection) =
    // Add Giraffe dependencies
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    |> ignore)
        .Build()
        .Run()
    0
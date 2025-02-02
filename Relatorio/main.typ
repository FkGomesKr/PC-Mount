#import "cover.typ": cover
#import "template.typ": *

#show: project

#cover(title: "PCMount", authors: (
  (name: "Fábio Magalhães", number: "A104365"), 
  (name: "João Machado", number: "A104084"),
  (name: "Pedro Gomes", number: "A104540"), 
  (name: "André Pinto", number: "A104267"),
  (name: "Ricardo Sousa", number: "A104524")), 
  "Outubro, 2024")

#set page(numbering: "i", number-align: center)
#counter(page).update(1)


#heading(numbering: none, outlined: false)[Resumo]
Este relatório descreve o desenvolvimento do sistema _*PCMount*_, projetado para otimizar os processos de gestão e montagem de computadores na _*SpaceEletronics*_, uma empresa do setor de eletrónica. O sistema tem como objetivo centralizar a gestão de encomendas, inventário e montagem, promovendo maior eficiência operacional e melhorando a experiência do cliente.

Durante o projeto, foi realizado o levantamento e análise de requisitos, especificação e modelação do software através de diagramas UML, e conceção de um modelo de dados robusto utilizando Microsoft SQL Server.

#v(2em)

*Área de Aplicação*: Processos de construção de software / Especificação e desenvolvimento de aplicações do mundo real.

*Palavras-Chave*: .NET, C\#, Base de Dados, Web, Web App, SQL Server, Entity Framework, ASP.NET Core, Razor, Blazor, HTML, CSS, Diagrama UML, Modelo Dominio, Entidades, Relacionamentos, Diagrama de Classes, Diagrama de Casos de Uso, Diagrama de Sequência, Engenharia Software, Interface do Utilizador, Sistema de Gestão, Linha de Montagem

*Sinónimos*: (Computador -> Produto), (Peças -> Componentes -> Partes), 
(Encomenda -> Pedido), (Stock -> Inventário), (Status -> Estado), (Processos -> Operações), 

#show outline: it => {
    show heading: set text(size: 18pt)
    it
}

#{
  show outline.entry.where(level: 1): it => {
    v(5pt)
    strong(it)
  }

  outline(
    title: [Índice], 
    indent: true, 
  )
}

#v(-0.4em)
#outline(
  title: none,
  target: figure.where(kind: "attachment"),
  indent: n => 1em,
)

#outline(
  title: [Índice de Figuras],
  target: figure.where(kind: image),
)

#outline(
  title: [Índice de Tabelas],
  target: figure.where(kind: table),
)

#set page(numbering: "1", number-align: center)
#counter(page).update(1)

#import "pages/mainPage.typ" as mainPage

#mainPage
# BestPath

Implementação dos algoritimos A*, BFS (Dijkstra), UCS (Busca uniforme) e DFS para encontrar o menor caminho entre dois pontos no mapa dos EUA.

O desafio foi originalmente proposto pelo DIMACS: [9th DIMACS Implementation Challenge - Shortest Paths](http://www.diag.uniroma1.it/challenge9/download.shtml), mas esta implementação foi feito como trabalho da disciplina de Inteligência Artificial (IA).

## Funcionalidades
- 🪀 Algoritimos implementados:
  - A*
    - Heuritica 1: Distância euclidiana
    - Heuritica 2: Distância de Haversine
  - BFS (Dijkstra)
  - UCS (Busca uniforme)
  - DFS
- ✨ CLI criada com [Spectre.Console](https://spectreconsole.net)
- ⏳ Timeout dinamicos (determinado de acordo com o a quantidade de algoritimos executados)
- 🛣️ Suporta execução assíncrona

### Mapas suportados/testados
|Nome |Descrição      |Nós         |Arestas     |
|-----|---------------|------------|------------|
| NY  | New York City | 264,346    | 733,846    |
| E   | Eastern USA   | 3,598,623  | 1,212,489  |
| W   | Western USA   | 6,262,104  | 15,248,146 |
| CTR | Central USA   | 14,081,816 | 34,292,496 |

## Resultados
Todos os testes foram executados no mapa W (Western USA).
<details>
  <summary>PC specs</summary>

- Intel Core I5 10210U
- Intel UHD Graphics
- 8GB RAM DDR4

</details>
Os algoritimos foram com um tempo maximo de 1 hora e de forma sincrona.
Algum pontos foram escolhidos de forma aleatória e outros foram escolhidos de forma que o algoritimo DFS encontrasse o caminho mais curto para que assim tenha amostras de casos para todos os algoritimos.

Os resultados foram organizados em uma planilha online para que seja atualizada constantimente até a data de entrega do trabalho.

[Relatório](https://docs.google.com/spreadsheets/d/17ncRLpKsQnp-vDf4RsPK0EYlOJ1Ba8K2WApR9-xmJg4/edit?usp=sharing)

# Como executar

## Requisitos
- [.NET 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- Mapas dos EUA (disponíveis no [site do desafio do DIMACS](http://www.diag.uniroma1.it/challenge9/download.shtml))

Antes de executar o programa é necessário extrair os arquivos do mapa para a pasta `input` na raiz do executavel para que o ele consiga ler. Não é nescessario extrair todos os mapas, apenas os que deseja executar.

O programa usa apenas o grafo de distancia (Distance graph) e as coordenadas (Coordinates), então no site do DIMACS é nescessario baixar apenas esses arquivos. Na raiz do executavel siga a seguinte estrutura de pastas:
> O nome dos arquivos também devem esta igual ao que esta descrito abaixo.

- `input/`
  - `center/`
    - `USA-center-coordinates`
    - `USA-center-distance`
  - `eastern/`
    - `USA-eastern-coordinates`
    - `USA-eastern-distance`
  - `ny/`
    - `USA-eastern-distance`
    - `USA-ny-distance`
  - `west/`
    - `USA-west-coordinates`
    - `USA-west-distance`

## Executando
Para executar o programa basta executar o seguinte comando na raiz do projeto:
```powershell
dotnet run --project BestPath/BestPath.csproj
```
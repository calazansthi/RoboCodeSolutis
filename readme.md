# Megatron

Abaixo estão listados as ações principais do robô proposto, de nome Megatron:

## Movimentação

Inicia a rodada fazendo movimentos circulares aleatórios para percorrer e escanear a arena.

## Ataque

Assim que o Megatron encontra um robô inimigo, ele trava a arma no inimigo e atira. Existem verificações para que o Megatron se mova caso o inimigo esteja numa distância não tão favorável para o tiro ou caso acabe saindo do campo de visão.

## Sobrevivência

Para a sobrevivência do Megatron, existem alguns eventos que disparam certas ações, são eles:

- Ao receber um tiro, o robô mira na direção que sofreu o tiro e se movimenta para poder sair da direção de outros projéteis e do campo de visão do inimigo.

- Ao bater na parede, o robô faz o movimento contrário ao que estava fazendo, se estiver indo para frente vai para trás e vice-versa.

- Ao bater em outro inimigo, o robô mira na direção do inimigo e atira

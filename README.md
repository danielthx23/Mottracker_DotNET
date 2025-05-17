# Mottracker

## Descrição do Projeto
Projeto para o Challenge da empresa Mottu, FIAP 2025.

A princípio, o projeto visa resolver o problema de organização da localização das motos nos pátios da Mottu, facilitando a gestão e monitoramento em tempo real dos veículos. Nossa solução utiliza câmeras equipadas com sensores de posicionamento que capturam a localização exata das motos. Cada moto possui um QR Code exclusivo para identificação individual, enquanto os pátios também são mapeados com QR Codes para facilitar a localização dos veículos em áreas específicas.

A API desenvolvida funcionará como uma plataforma integrada que une os dados gerados pelos dispositivos IoT das câmeras com a infraestrutura de armazenamento adequada para cada tipo de dado: os dados de IoT serão armazenados em um banco NoSQL, garantindo alta performance e escalabilidade para o processamento de grandes volumes de dados em tempo real; já os dados estruturados, como informações de motos, pátios, usuários, contratos e dashboards, serão mantidos em um banco de dados relacional.

Essa integração permite o acompanhamento em tempo real das motos via aplicativo, promovendo maior eficiência operacional, segurança e organização.

Além disso, o sistema gerencia cadastro e edição de dados, no futuro vai oferecer funcionalidades para autenticação e autorização dos usuários, gestão de permissões e geração de relatórios através de dashboards customizáveis, que auxiliam a equipe da Mottu a tomar decisões baseadas em dados confiáveis e atualizados.

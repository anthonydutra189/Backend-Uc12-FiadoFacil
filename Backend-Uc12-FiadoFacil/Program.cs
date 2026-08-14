using System;
using System.Collections.Generic;
using Backend_Uc12_FiadoFacil.Models;
using Backend_Uc12_FiadoFacil.Repositories;

namespace Backend_Uc12_FiadoFacil
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA FIADO FÁCIL ===");
                Console.WriteLine("1 - Gerenciar Usuários (User)");
                Console.WriteLine("2 - Gerenciar Empresas (Company)");
                Console.WriteLine("3 - Gerenciar Produtos (Product)");
                Console.WriteLine("4 - Gerenciar Pagamentos (Payment)");
                Console.WriteLine("5 - Gerenciar Produtos x Pagamentos");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuUsuarios();
                        break;
                    case "2":
                        MenuEmpresas();
                        break;
                    case "3":
                        MenuProdutos();
                        break;
                    case "4":
                        MenuPagamentos();
                        break;
                    case "5":
                        MenuProdutoPagamentos();
                        break;
                    case "0":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ======================= ROTAS DE USUÁRIOS =======================
        static void MenuUsuarios()
        {
            var repo = new UserRepository();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- GERENCIAR USUÁRIOS ---");
                Console.WriteLine("1 - Inserir Usuário");
                Console.WriteLine("2 - Listar Usuários");
                Console.WriteLine("3 - Atualizar Usuário");
                Console.WriteLine("4 - Deletar Usuário");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha: ");
                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        var u = new User();
                        Console.Write("Nome: "); u.Name = Console.ReadLine() ?? "";
                        Console.Write("Tipo (Customer/Admin/Client): "); u.Type = Console.ReadLine() ?? "";
                        Console.Write("Email: "); u.Email = Console.ReadLine() ?? "";
                        Console.Write("Senha: "); u.Senha = Console.ReadLine() ?? "";
                        repo.Insert(u);
                        Console.WriteLine($"Usuário cadastrado com ID {u.Id}. Pressione algo..."); Console.ReadKey();
                        break;
                    case "2":
                        var users = repo.GetAll();
                        foreach (var user in users)
                            Console.WriteLine($"ID: {user.Id} | Nome: {user.Name} | Tipo: {user.Type} | Email: {user.Email}");
                        Console.WriteLine("Pressione algo..."); Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("ID do usuário a atualizar: ");
                        if (int.TryParse(Console.ReadLine(), out int uid))
                        {
                            var up = repo.GetById(uid);
                            if (up != null)
                            {
                                Console.Write($"Nome ({up.Name}): "); var n = Console.ReadLine(); if (!string.IsNullOrEmpty(n)) up.Name = n;
                                Console.Write($"Tipo ({up.Type}): "); var t = Console.ReadLine(); if (!string.IsNullOrEmpty(t)) up.Type = t;
                                Console.Write($"Email ({up.Email}): "); var e = Console.ReadLine(); if (!string.IsNullOrEmpty(e)) up.Email = e;
                                Console.Write($"Senha ({up.Senha}): "); var s = Console.ReadLine(); if (!string.IsNullOrEmpty(s)) up.Senha = s;
                                repo.Update(up);
                                Console.WriteLine("Atualizado com sucesso. Pressione algo...");
                            }
                            else Console.WriteLine("Não encontrado.");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("ID do usuário a deletar: ");
                        if (int.TryParse(Console.ReadLine(), out int did))
                        {
                            repo.Delete(did);
                            Console.WriteLine("Deletado com sucesso. Pressione algo...");
                        }
                        Console.ReadKey();
                        break;
                    case "0": return;
                    default: break;
                }
            }
        }

        // ======================= ROTAS DE EMPRESAS =======================
        static void MenuEmpresas()
        {
            var repo = new CompanyRepository();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- GERENCIAR EMPRESAS ---");
                Console.WriteLine("1 - Inserir Empresa");
                Console.WriteLine("2 - Listar Empresas");
                Console.WriteLine("3 - Atualizar Empresa");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha: ");
                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        var c = new Company();
                        Console.Write("Nome: "); c.Name = Console.ReadLine() ?? "";
                        Console.Write("Categoria: "); c.Category = Console.ReadLine() ?? "";
                        Console.Write("CNPJ: "); c.Cnpj = Console.ReadLine() ?? "";
                        Console.Write("Places: "); c.Places = Console.ReadLine() ?? "";
                        Console.Write("ZipCode: "); c.ZipCode = Console.ReadLine() ?? "";
                        Console.Write("Endereço: "); c.Addrres = Console.ReadLine() ?? "";
                        Console.Write("Telefone: "); c.Phone = Console.ReadLine() ?? "";
                        Console.Write("ID do Usuário Dono: "); if (int.TryParse(Console.ReadLine(), out int uid)) c.UserId = uid;
                        repo.Insert(c);
                        Console.WriteLine($"Empresa cadastrada com ID {c.Id}. Pressione algo..."); Console.ReadKey();
                        break;
                    case "2":
                        var comps = repo.GetAll();
                        foreach (var comp in comps)
                            Console.WriteLine($"ID: {comp.Id} | Nome: {comp.Name} | DonoID: {comp.UserId}");
                        Console.WriteLine("Pressione algo..."); Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("ID da empresa a atualizar: ");
                        if (int.TryParse(Console.ReadLine(), out int cid))
                        {
                            var up = repo.GetById(cid);
                            if (up != null)
                            {
                                Console.Write($"Nome ({up.Name}): "); var n = Console.ReadLine(); if (!string.IsNullOrEmpty(n)) up.Name = n;
                                Console.Write($"Telefone ({up.Phone}): "); var tel = Console.ReadLine(); if (!string.IsNullOrEmpty(tel)) up.Phone = tel;
                                repo.Update(up);
                                Console.WriteLine("Atualizado com sucesso. Pressione algo...");
                            }
                            else Console.WriteLine("Não encontrado.");
                        }
                        Console.ReadKey();
                        break;
                    case "0": return;
                    default: break;
                }
            }
        }

        // ======================= ROTAS DE PRODUTOS =======================
        static void MenuProdutos()
        {
            var repo = new ProductRepository();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- GERENCIAR PRODUTOS ---");
                Console.WriteLine("1 - Inserir Produto");
                Console.WriteLine("2 - Listar Produtos");
                Console.WriteLine("3 - Atualizar Produto");
                Console.WriteLine("4 - Deletar Produto");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha: ");
                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        var p = new Product();
                        Console.Write("Nome: "); p.Name = Console.ReadLine() ?? "";
                        Console.Write("Tipo: "); p.Type = Console.ReadLine() ?? "";
                        Console.Write("Valor: "); if (double.TryParse(Console.ReadLine(), out double v)) p.Value = v;
                        Console.Write("Descrição: "); p.Description = Console.ReadLine() ?? "";
                        Console.Write("URL Img: "); p.UrlImg = Console.ReadLine() ?? "";
                        Console.Write("ID Empresa: "); if (int.TryParse(Console.ReadLine(), out int cid)) p.CompanyId = cid;
                        repo.Insert(p);
                        Console.WriteLine($"Produto cadastrado ID {p.Id}. Pressione algo..."); Console.ReadKey();
                        break;
                    case "2":
                        var prods = repo.GetAll();
                        foreach (var prod in prods)
                            Console.WriteLine($"ID: {prod.Id} | Nome: {prod.Name} | Valor: {prod.Value} | EmpresaID: {prod.CompanyId}");
                        Console.WriteLine("Pressione algo..."); Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("ID do produto a atualizar: ");
                        if (int.TryParse(Console.ReadLine(), out int pid))
                        {
                            var up = repo.GetById(pid);
                            if (up != null)
                            {
                                Console.Write($"Nome ({up.Name}): "); var n = Console.ReadLine(); if (!string.IsNullOrEmpty(n)) up.Name = n;
                                Console.Write($"Valor ({up.Value}): "); var val = Console.ReadLine(); if (double.TryParse(val, out double nv)) up.Value = nv;
                                repo.Update(up);
                                Console.WriteLine("Atualizado. Pressione algo...");
                            }
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Write("ID do produto a deletar: ");
                        if (int.TryParse(Console.ReadLine(), out int did)) repo.Delete(did);
                        Console.WriteLine("Feito. Pressione algo..."); Console.ReadKey();
                        break;
                    case "0": return;
                    default: break;
                }
            }
        }

        // ======================= ROTAS DE PAGAMENTOS =======================
        static void MenuPagamentos()
        {
            var repo = new PaymentRepository();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- GERENCIAR PAGAMENTOS ---");
                Console.WriteLine("1 - Inserir Pagamento");
                Console.WriteLine("2 - Listar Pagamentos");
                Console.WriteLine("3 - Deletar Pagamento");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha: ");
                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        var p = new Payment();
                        Console.Write("Valor: "); if (double.TryParse(Console.ReadLine(), out double v)) p.Value = v;
                        Console.Write("Método: "); p.Method = Console.ReadLine() ?? "";
                        Console.Write("ToDate (dd/mm/aaaa): "); if (DateTime.TryParse(Console.ReadLine(), out DateTime td)) p.ToDate = td;
                        Console.Write("DueDate (dd/mm/aaaa): "); if (DateTime.TryParse(Console.ReadLine(), out DateTime dd)) p.DueDate = dd;
                        Console.Write("ID Usuário: "); if (int.TryParse(Console.ReadLine(), out int uid)) p.UserId = uid;
                        Console.Write("ID Empresa: "); if (int.TryParse(Console.ReadLine(), out int cid)) p.CompanyId = cid;
                        repo.Insert(p);
                        Console.WriteLine($"Cadastrado ID {p.Id}. Pressione algo..."); Console.ReadKey();
                        break;
                    case "2":
                        var pays = repo.GetAll();
                        foreach (var pay in pays)
                            Console.WriteLine($"ID: {pay.Id} | Valor: {pay.Value} | Método: {pay.Method}");
                        Console.WriteLine("Pressione algo..."); Console.ReadKey();
                        break;
                    case "3":
                        Console.Write("ID do pagamento a deletar: ");
                        if (int.TryParse(Console.ReadLine(), out int did)) repo.Delete(did);
                        Console.WriteLine("Feito. Pressione algo..."); Console.ReadKey();
                        break;
                    case "0": return;
                    default: break;
                }
            }
        }

        // ======================= ROTAS PRODUTO x PAGAMENTO =======================
        static void MenuProdutoPagamentos()
        {
            var repo = new ProductPaymentRepository();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- VINCULAR PRODUTO E PAGAMENTO ---");
                Console.WriteLine("1 - Inserir Vinculo");
                Console.WriteLine("2 - Listar Produtos de um Pagamento");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha: ");
                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.Write("ID Pagamento: "); int payId; int.TryParse(Console.ReadLine(), out payId);
                        Console.Write("ID Produto: "); int prodId; int.TryParse(Console.ReadLine(), out prodId);
                        repo.Insert(prodId, payId);
                        Console.WriteLine("Vinculado! Pressione algo..."); Console.ReadKey();
                        break;
                    case "2":
                        Console.Write("ID Pagamento: "); int pId; int.TryParse(Console.ReadLine(), out pId);
                        var ids = repo.GetProductIdsByPaymentId(pId);
                        Console.WriteLine($"Produtos vinculados ao pagamento {pId}: " + string.Join(", ", ids));
                        Console.WriteLine("Pressione algo..."); Console.ReadKey();
                        break;
                    case "0": return;
                    default: break;
                }
            }
        }
    }
}

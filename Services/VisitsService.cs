using System.Data.Common;
using APBD_k1_GrupaB.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace APBD_k1_GrupaB.Services;

public class VisitsService : IVisitsService
{
    private readonly string _connectionString;

    public VisitsService()
    {
        _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;";

    }
    
    // 1. GET api/visits/{id}
    public async Task<ClientVisitDto> GetVisitByClientIdAsync(int id, CancellationToken cancellationToken)
    {
        ClientVisitDto clientVisitDto = new ClientVisitDto();
        // visitDto.client = new ClientDto();
        // visitDto.mechanic = new MechanicDto();
        // visitDto.visitServices = new List<VisitServiceDto>();
        
        string sqlQuery = @"
                                SELECT v.date,
                                       c.first_name, c.last_name, c.date_of_birth,
                                        m.mechanic_id, m.licence_number,
                                        s.name,
                                        vi.service_fee
                                FROM Visit v
                                JOIN Client c ON c.client_id = v.client_id
                                JOIN Mechanic m ON m.mechanic_id = v.mechanic_id
                                JOIN Visit_Service vi ON vi.visit_id = v.visit_id
                                JOIN Service s ON s.service_id = vi.service_id
                                WHERE c.client_id = @id;
                                        
";

        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            
            await conn.OpenAsync(cancellationToken);

            using (SqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    clientVisitDto.date = (DateTime)reader["date"];

                    clientVisitDto.client = new ClientDto()
                    {
                        firstName = (string)reader["first_name"],
                        lastName = (string)reader["last_name"],
                        dateOfBirth = (DateTime)reader["date_of_birth"]
                    };

                    clientVisitDto.mechanic = new MechanicDto()
                    {
                        mechanic_id = (int)reader["mechanic_id"],
                        licence_number = (string)reader["licence_number"]
                    };

                    if (clientVisitDto.visitServices == null)
                    {
                        clientVisitDto.visitServices = new List<VisitServiceDto>();
                    }
                    clientVisitDto.visitServices.Add(
                        new VisitServiceDto()
                        {
                            name = (string)reader["name"],
                            service_fee = (decimal)reader["service_fee"]
                        }
                    );
                    
                }
            }
        }
        
        return clientVisitDto;
    }
    
    // 2. POST api/visits
    // public async Task<int> AddVisitAsync(VisitDto visitDto, CancellationToken cancellationToken)
    // {
    //     await using SqlConnection conn = new SqlConnection(_connectionString);
    //     await using SqlCommand cmd = new SqlCommand();
    //     
    //     cmd.Connection = conn;
    //     await conn.OpenAsync(cancellationToken);
    //     
    //     DbTransaction transaction = await conn.BeginTransactionAsync();
    //     cmd.Transaction = transaction as SqlTransaction;
    //     
    //     
    //     
    //     string sqlQuery = @"
    //                     INSERT INTO Visit(visit_id, client_id, mechanic_id, date)
    //                     VALUES(@id, @client_id, @mechanic_id, @date);)
    //     ";
    // }
}
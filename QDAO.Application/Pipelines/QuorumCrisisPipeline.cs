using QDAO.Persistence.Repositories.ProposalQuorum;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Pipelines
{
    // Нужен только для того чтобы слать уведомления
    public class QuorumCrisisPipeline
    {
        private readonly ProposalQrisisQueueRepository _repository;
        public async Task PipeAsync(CancellationToken stoppingToken)
        {

            ProposalWIthQueueId proposal;
            while(!stoppingToken.IsCancellationRequested && (proposal = await _repository.GetNext(stoppingToken)) != default)
            {
                // Получаем информацию по пропозалу с датой конца 

                // Получаем статус пропозала

                // Если статус пропозала - отвергнут по кворуму то 

                // Шлем нотификацию принципалам и админу

                // нужно научиться показывать пропозалы в этом статусе

                // нужен метод подписи аппрува 

                // 
            }
        }
    }
}

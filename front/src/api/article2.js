import request from '@/utils/request2'

export function fetchList2(query) {
  return request({
    url: '/api/v1/Wf/GetPageList?page=1&rows=1000&sort=id&order=asc',
    method: 'get',
    params: query
  })
}

import request from '@/utils/request2'

export function fetchList2(query) {
  return request({
    url: '/api/v1/Wf/GetPageList',
    method: 'get',
    params: query
  })
}

export function getById(query) {
  return request({
    url: '/api/v1/Wf/GetById',
    method: 'get',
    params: query
  })
}

export function update2(query) {
  return request({
    url: '/api/v1/Wf/Update2',
    method: 'get',
    params: query
  })
}


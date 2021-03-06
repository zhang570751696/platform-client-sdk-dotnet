---
title: LineIntegration
---
## ININ.PureCloudApi.Model.LineIntegration

## Properties

|Name | Type | Description | Notes|
|------------ | ------------- | ------------- | -------------|
| **Id** | **string** | A unique Integration Id | |
| **Name** | **string** | The name of the LINE Integration | |
| **ChannelId** | **string** | The Channel Id from LINE messenger | |
| **WebhookUri** | **string** | The Webhook URI to be updated in LINE platform | |
| **Status** | **string** | The status of the LINE Integration | [optional] |
| **Recipient** | [**DomainEntityRef**](DomainEntityRef.html) | The recipient associated to the Line Integration. This recipient is used to associate a flow to an integration | [optional] |
| **DateCreated** | **DateTime?** | Date this Integration was created. Date time is represented as an ISO-8601 string. For example: yyyy-MM-ddTHH:mm:ss.SSSZ | [optional] |
| **DateModified** | **DateTime?** | Date this Integration was modified. Date time is represented as an ISO-8601 string. For example: yyyy-MM-ddTHH:mm:ss.SSSZ | [optional] |
| **CreatedBy** | [**DomainEntityRef**](DomainEntityRef.html) | User reference that created this Integration | [optional] |
| **ModifiedBy** | [**DomainEntityRef**](DomainEntityRef.html) | User reference that last modified this Integration | [optional] |
| **Version** | **int?** | Version number required for updates. | |
| **SelfUri** | **string** | The URI for this object | [optional] |
{: class="table table-striped"}


